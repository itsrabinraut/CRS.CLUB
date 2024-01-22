function generatePaginationNumbers(totalData, currentPage, currentUrl, id, limit = 10) {
    var entriesPerPage = limit;
    var totalPages = Math.ceil(totalData / entriesPerPage);
    var paginationNumbers = document.getElementById(id);
    paginationNumbers.innerHTML = '';
    var maxVisiblePages = 3;
    var startPage = Math.max(1, currentPage - Math.floor(maxVisiblePages / 2));
    var endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);
    if (currentPage == '') {

    } else {
        var pageLink = document.createElement('a');
        pageLink.href = currentUrl + '?Offset=' + (parseInt(currentPage) - 1) + '&Limit=' + entriesPerPage;
        //pageLink.className = (i === currentPage) ? 'pagination-number active' : 'pagination-number';
        pageLink.innerText = "Prev";
        paginationNumbers.appendChild(pageLink);
    }
    for (var i = startPage; i <= endPage; i++) {
        var pageLink = document.createElement('a');
        pageLink.href = currentUrl + '?Offset=' + i + '&Limit=' + entriesPerPage;
        //pageLink.className = (i === currentPage) ? 'pagination-number active' : 'pagination-number';
        pageLink.innerText = i;
        paginationNumbers.appendChild(pageLink);
    }
    if (startPage > 1) {
        var Start = document.createElement('span');
        Start.innerText = '...';
        paginationNumbers.insertBefore(Start, paginationNumbers.firstChild);
    }
    if (endPage < totalPages) {
        var End = document.createElement('span');
        End.innerText = '...';
        paginationNumbers.appendChild(End);
    }
    pageLink = document.createElement('a');
    if (currentPage == '') {
        pageLink.href = currentUrl + '?Offset=' + (2) + '&Limit=' + entriesPerPage;
        pageLink.innerText = 'Next';
        paginationNumbers.appendChild(pageLink);
    }
    else if (currentPage == totalPages) {

    }
    else {
        pageLink.href = currentUrl + '?Offset=' + (parseInt(currentPage) + 1) + '&Limit=' + entriesPerPage;
        pageLink.innerText = 'Next';
        paginationNumbers.appendChild(pageLink);
    }
}