document.addEventListener("DOMContentLoaded", function () {
  loadAuthors();

  // Add event listener for the save author button
  document.getElementById("saveAuthorBtn").addEventListener("click", addAuthor);
});

async function loadAuthors() {
  try {
    const response = await fetch("http://localhost:5214/api/authors");
    const { data } = await response.json();

    const tableBody = document.getElementById("authorsTableBody");
    tableBody.innerHTML = "";

    data.forEach((author) => {
      const row = document.createElement("tr");
      row.innerHTML = `
                <td>${author.firstName}</td>
                <td>${author.lastName}</td>
                <td>
                    <button class="btn btn-sm btn-danger" onclick="deleteAuthor(${author.id})">Delete</button>
                </td>
            `;
      tableBody.appendChild(row);
    });
  } catch (error) {
    console.error("Error loading authors:", error);
    alert("Error loading authors. Please try again.");
  }
}

async function addAuthor() {
  const firstName = document.getElementById("firstName").value;
  const lastName = document.getElementById("lastName").value;

  const authorData = {
    firstName: firstName,
    lastName: lastName,
  };

  try {
    const response = await fetch("http://localhost:5214/api/authors", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(authorData),
    });

    if (response.ok) {
      // Close the modal
      const modal = bootstrap.Modal.getInstance(
        document.getElementById("addAuthorModal")
      );
      modal.hide();

      // Clear the form
      document.getElementById("addAuthorForm").reset();

      // Reload the authors list
      loadAuthors();
    } else {
      const error = await response.json();
      alert("Error adding author: " + error.message);
    }
  } catch (error) {
    console.error("Error adding author:", error);
    alert("Error adding author. Please try again.");
  }
}

async function deleteAuthor(id) {
  if (confirm("Are you sure you want to delete this author?")) {
    try {
      const response = await fetch(`http://localhost:5214/api/authors/${id}`, {
        method: "DELETE",
      });

      if (response.ok) {
        loadAuthors();
      } else {
        const error = await response.json();
        alert("Error deleting author: " + error.message);
      }
    } catch (error) {
      console.error("Error deleting author:", error);
      alert("Error deleting author. Please try again.");
    }
  }
}
