function onBookDelete(elem) {
    let response = window.confirm("This book has stock. Are you sure you want to delete?");
    if (response) {
       elem.parentElement.submit();
    }
}
