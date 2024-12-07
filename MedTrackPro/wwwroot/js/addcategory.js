let addCategoryBtn = document.getElementById('addCategoryBtn');
console.log("Link Successfully")
addCategoryBtn.addEventListener('click', async () => {
    let categoryName = document.getElementById('categoryName').value;
    if (!categoryName.trim()) {
        alert("Please enter valid category");
        return;
    }
    console.log(categoryName);
    try {
        let response = await fetch('/HospitalUser/AddNewCategory', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ categoryName })
        });

        if (response.ok) {
            let result = await response.json();

            alert(result.message || "Category added successfully!");
            // Optionally, clear the input field or update the UI
            document.getElementById('categoryName').value = '';
            location.reload();
        }
        else {
            let error = await response.json();
            alert(error.message || "Failed to add category.");
        }
    }
    catch (error) {
        console.error("Error adding category:", error);
        alert("An error occurred while adding the category.");
    }
});