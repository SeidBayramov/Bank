let deleteBtn = document.querySelectorAll(".item-delete")

deleteBtn.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();

    Swal.fire({
        title: "Silmek isteyirsiniz?",
        text: "Silirem geri donusu olmayacaq",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sil!!"
    }).then((result) => {
        if (result.isConfirmed) {
            let url = btn.getAttribute("href")
            fetch(url)
                .then(response => {
                    if (response.status == 200) {
                        Swal.fire({
                            title: "Silindi",
                            text: "Your file has been deleted.",
                            icon: "success"
                        });
                        btn.parentElement.parentElement.remove()
                    }
                    else {
                        Swal.fire({
                            title: "Deleted!",
                            text: "Ozun bilersen !!!!",
                            icon: "error"
                        });
                    }
                })
        }
    });
}))