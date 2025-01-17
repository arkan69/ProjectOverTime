﻿//$(document).ready(function () {
    var table_splk = $("#table_splk").DataTable({
        ajax: {
            "url": "../Employee/GetMasterEmployee",
            "dataType": "Json",
            "dataSrc": ""
        },
        columns: [
            {
                "data": "id",
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "nik"
            },
            {
                "data": null,
                render: function (data, type, row, meta) {
                    if (row['overtimeType'] == 0) {
                        return "Weekdays"
                    } else {
                        return "Weekends/Holiday"
                    }
                }
            },
            {
                "data": null,
                render: function (data, type, row, meta) {
                    var startdate = row['startDate'];
                    var tmp = new Date(startdate);
                    return ((tmp.getMonth() > 8) ? (tmp.getMonth() + 1) : ('0' + (tmp.getMonth() + 1))) + '/' + ((tmp.getDate() > 9) ? tmp.getDate() : ('0' + tmp.getDate())) + '/' + tmp.getFullYear();
                }
            },
            {
                "data": null,
                render: function (data, type, row, meta) {
                    var start_time = row['startDate'];
                    var tmp = Waktu(start_time);
                    return tmp;
                }
            },
            {
                "data": null,
                render: function (data, type, row, meta) {
                    var end_time = row['endDate'];
                    var tmp = Waktu(end_time);
                    return tmp;
                }
            },
            {
                "data": "description"
            },
            {
                "data": null,
                render: function (data, type, row, meta) {
                    switch (row['status']) {
                        case 0:
                            return `<div class="badge bg-secondary text-wrap" style="width: 6rem;">
                                      Pending
                                    </div>`
                            break;
                        case 1:
                            return `<div class="badge bg-danger text-wrap" style="width: 6rem;">
                                      Refuse
                                    </div>`
                            break;
                        case 2:
                            return `<div class="badge bg-info text-wrap" style="width: 6rem;">
                                      Approve
                                    </div>`
                            break;
                        default:
                            return `<div class="badge bg-success text-wrap" style="width: 6rem;">
                                      Done
                                    </div>`
                    }

                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    var getNik = row['id'];
                    if (row['status'] != 0) {
                        var Status = 'Disabled';
                    }
                    return `<div class="btn-group">
                                    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-toggle="tooltip" onclick="detailSplk('${getNik}')" data-bs-target="#detailModal" data-placement="top" title="Detail" title="Detail">
                                        <span class="fas fa-magnifying-glass"></span>
                                    </button>
                                    &nbsp;
                                    <button type="button" class="btn btn-danger" data-toggle="tooltip" data-placement="top" title="Delete" onclick="DeleteSpkl('${getNik}')" ${Status}>
                                        <span class="fas fa-trash"></span>
                                    </button>
                                </div>
                            `;
                },
                "orderable": false
            }
        ],

        //dom: '<"top"Blf>rtip',
        
    });
//});


$('#table_splk').on('click', 'td', function () {
    var colIndex = $(this).index();
    console.log("Column index: ", colIndex);
});

//Validate Jquery Plugin
//Ver 0
$(document).ready(function () {
    $('#formSplk').validate({
        ignore: ":hidden",
        rules: {
            jenislembur: {
                required: true
            },
            tglmulai: {
                required: true
            },
            jammulai: {
                required: true
            },
            jamselesai: {
                required: true
            },
            deskripsi: {
                required: true
            },
            buktifile: {
                required: true
            }
        },

        messages: {
            jenislembur: {
                required: "<div style='font-size:15px; '>Overtime Type is required.</div>"
            },
            tglmulai: {
                required: "<div style='font-size:15px; '>Date is required.</div>"
            },
            jammulai: {
                required: "<div style='font-size:15px; '>Start Time is required.</div>"
            },
            jamselesai: {
                required: "<div style='font-size:15px; '>End Time is required.</div>"
            },
            deskripsi: {
                required: "<div style='font-size:15px; '>Description is required.</div>"
            },
            buktifile: {
                required: "<p style='font-size:15px; width:100%'>Evidence is required.</p>"
            }
        },
        highlight: function (element) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },

        errorPlacement: function (error, element) {
            error.insertAfter(element);
        }
    });
});

// Action Function Insert & Update
$("#btnSaveSplk").click(function (e) {
    e.preventDefault();
    if ($("#formSplk").valid()) {
        var data_action = $(this).attr("data-name");
        if (data_action == "insert") {
            console.log("INI INSERT");
            InsertSplkForm();
        }
    }
});

// Clear Modal Insert Employee
function InsertSplk() {
    $('#nik').val("");
    $('#jenislembur').val("");
    $('#tglmulai').val("");
    $('#jammulai').val("");
    $('#jamselesai').val("");
    $('#deskripsi').val("");
    $('#buktifile').val("");
    $('#btnSaveSplk').attr('data-name', 'insert').html("<span class='fas fa-save'>&nbsp;</span>Save");

    //get NIK
    $.ajax({
        url: '../Employee/GetNIK'
    }).done((result) => {
        $('#nik').val(result.nik);
        $('#nik').prop('readonly', true);
    })

};


//INSERT NEW 1
function InsertSplkForm() {
    let totJam = 0;
    let Data;
    $.ajax({
        url: "../Employee/GetMasterEmployee"
    }).done((result) => {
        Data = result;
        for (const data of result) {
            if (data.status == 2 || data.status == 3) {
                totJam += data.jmlJam;
            }
        }
        let Start = $("#tglmulai").val() + 'T' + $("#jammulai").val();
        let End = $("#tglmulai").val() + 'T' + $("#jamselesai").val();
        let today = new Date();

        const year = today.getFullYear();
        const month = today.getMonth(); // returns the month (0-11)
        const ValidateYear = new Date(Start);

        //Hitung selisih Jam
        let duration = (new Date(End)).getTime() - (new Date(Start)).getTime();
        let durationMinutes = duration / (1000 * 60);
        let hours = Math.floor(durationMinutes / 60);


        //Data from API validate Tanggal yang sama.
        let flag = false;
        for (var i = 0; i < Data.length; i++) {
            if (Tanggal(Data[i].startDate) == Tanggal(Start)) {
                flag = true;
                break;
            }
        }
        console.log("testing", year, ":", ValidateYear.getFullYear(), ":", month, ":", ValidateYear.getMonth());
        console.log("testing2", year >= ValidateYear.getFullYear());
        console.log("testing3", month <= ValidateYear.getMonth());
        var fd = new FormData();
        fd.append('nik', $("#nik").val())
        fd.append('overtimeType', $('#jenislembur').val());
        fd.append('StartDate', Start);
        fd.append('EndDate', End);
        fd.append('description', $("#deskripsi").val());
        fd.append('JmlJam', hours);
        fd.append('file', $('#buktifile')[0].files[0]);

        if ((new Date(Start)).getTime() > today.getTime()) {
            alert("Tanggal tidak boleh melebihi hari ini!");
        }
        else if (($('#jenislembur').val() == 0) && (hours < 1 || hours > 4)) {
            alert("Pengambilan lembur Kerja min 1 jam atau max 4 jam!");
        }
        else if (($('#jenislembur').val() == 1) && (hours < 1 || hours > 12)) {
            alert("Pengambilan lembur Libur min 1 jam atau max 12 jam!");
        }
        else if (totJam > 46) {
            alert("Pemgambilan Lembur tidak boleh lebih dari 46 jam dalam 1 bulan!");
        }
        else if (ValidateYear.getFullYear() !== year || ValidateYear.getMonth()  !== month ) {
            alert("Pemgambilan Lembur hanya bisa dilakukan pada bulan dan tahun ini!");
        }
        else if (flag) {
            alert("Pemgambilan Lembur tidak bisa dilakukan pada tanggal yang sama!");
        } else {
            $.ajax({
                url: "../Splk/SplkForm",
                type: "POST",
                data: fd,
                processData: false,
                contentType: false,
            }).done((result) => {
                Swal.fire(
                    'Success',
                    "Request added Successfully",
                    'success'
                )
                $('.insertModalSplk').modal('hide');
                table_splk.ajax.reload();
            })
        }

    });
    
};

//UPDATE
function detailSplk(key) {
    console.log(key);
    $.ajax({
        url: 'https://localhost:7092/api/Splks/' + key
    }).done((result) => {
        console.log(result);

        //$('#detailnik').prop('readonly', true);
        //$('#detailnik').val(result.data.nik).readonly;
        document.getElementById('detailniks').innerHTML = result.data.nik;
        if (result.data.overtimeType == 0) {
            //$('#detailjenislembur').val("Kerja");
            document.getElementById('detailjenislemburs').innerHTML = "Work";
        } else {
            //$('#detailjenislembur').val("Libur");
            document.getElementById('detailjenislemburs').innerHTML = "Holiday";
        }

        startdate_modified = Tanggal(result.data.startDate);
        //$('#detailtglmulai').val(startdate_modified);
        document.getElementById('detailtglmulais').innerHTML = startdate_modified;

        st_modified = Waktu(result.data.startDate);
        //$('#detailjammulai').val(st_modified);
        document.getElementById('detailjammulais').innerHTML = st_modified;

        ed_modified = Waktu(result.data.endDate);
        //$('#detailjamselesai').val(ed_modified);
        document.getElementById('detailjamselesais').innerHTML = ed_modified;

        //$('#detaildeskripsi').val(result.data.description);
        document.getElementById('detaildeskripsis').innerHTML = result.data.description;

        imgElem.setAttribute('src', "data:application/pdf;base64," + result.data.proofOvertime);

    }).fail((error) => {
        console.log(error);
        Swal.fire(
            'Opps!',
            'Something went wrong!',
            'error'
        )
    });
}

function Tanggal(tgl) {
    var tmp = new Date(tgl);
    var startdate_modified = tmp.getFullYear() + '-' + ((tmp.getMonth() > 8) ? (tmp.getMonth() + 1) : ('0' + (tmp.getMonth() + 1))) + '-' + ((tmp.getDate() > 9) ? tmp.getDate() : ('0' + tmp.getDate()));
    return startdate_modified;
}

function Waktu(waktu) {
    var time = new Date(waktu);
    var time_modified = ((time.getHours() < 10) ? ('0' + time.getHours()) : (time.getHours())) + ":" + ((time.getMinutes() < 10) ? ('0' + time.getMinutes()) : (time.getMinutes()));
    return time_modified;
}



var downloadButton = document.getElementById("download-pdf-employ");
if (downloadButton) {
    downloadButton.addEventListener("click", function (event) {
        event.preventDefault();
        var image = document.getElementById("imgElem");
        var base64string = image.src;
        var link = document.createElement("a");
        link.download = "filebukti.pdf";
        link.href = base64string;
        link.click();
    })
} else {
    console.error('Elemen tidak ditemukan');
};

//Detele data
const DeleteSpkl = (key) => {
    Swal.fire({
        title: 'Are you sure?',
        text: 'You want able to revert this!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: `https://localhost:7092/api/Splks/${key}`,
                success: () => {
                    Swal.fire(
                        'Deleted',
                        'Employee has been deleted.',
                        'success'
                    )
                    table_splk.ajax.reload()
                },
                error: () => {
                    Swal.fire(
                        'Failed',
                        'Error deleting splk employee',
                        'error'
                    )
                }
            })
        }
    })
}



$.ajax({
    url: "../Employee/GetName"
}).done((result) => {
    
    console.log(result);
    $("#nameaccount").append(`<span class='mr - 2 d - none d - lg - inline text - black - 600 small'>${result[0].fullName} </span>`);
})