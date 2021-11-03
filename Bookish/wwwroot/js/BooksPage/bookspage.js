function onBookDelete(availableCopies) {
    if (availableCopies > 0) {
        let response = window.confirm("This book has stock. Are you sure you want to delete?");
        if (!response) {
            return;
        }
    }
    
    document.getElementById("deleteBookForm").submit();
}
