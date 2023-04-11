
$(document).ready(function () {
  var table = null;
  $.ajax({
    url: 'EmployeeData_07_04_2023.json',
    dataType: 'json',
    success: function (data) {

      table = $('#EmployeeTable').DataTable({
        data: data,
        "lengthChange": false,
        language: {
          searchPlaceholder: 'Search here...',
          search: "",
        },
        bInfo: false,
        columns: [{
            data: 'EmployeeID',
            sorting: false,
            orderable: false
          },
          {
            data: 'Name',
          },
          {
            data: 'Department',
            type: 'html',

            render: function (data, type, row) {
              switch (data) {
                case 'Sales':
                  return '<span style="color:#ff0000">Sales</span>';
                case 'Marketing':
                  return '<span style="color:#005400">Marketing</span>';
                case 'Development':
                  return '<span style="color:#000">Development</span>';
                case 'QA':
                  return '<span style="color:#0000ff">QA</span>';
                case 'HR':
                  return '<span style="color:#fe00ef">HR</span>';
                case 'SEO':
                  return '<span style="color:pink">SEO</span>';
                default:
                  return data;
              }
            }
          },
          {
            data: 'Email',
            render: function (data, type, row) {
              return '<a href="mailto:' + data + '">' + data + '</a>';
            }
          },
          {
            data: 'PhoneNumber',
            orderable: false,
            render: function (data, type, row) {
              return '<a href="tel:' + data + '">' + data + '</a>';
            }
          },
          {
            data: 'Gender',
            orderable: false,
            render: function (data, type, row) {
              return data === 0 ? 'M' : 'F';
            }
          },

          {
            data: null,
            orderable: false,
            render: function (data, type, row) {
              return '<i class="fas fa-eye SeeDetails"></i>'
            }
          },
        ]
      });

      $('#EmployeeTable tbody').on('click', '.SeeDetails', function () {
        var data = table.row($(this).parents('tr')).data();
        var modal = $('#EmployeeDetailsModal');

        var dob = new Date(data.DOB);
        var day = dob.getDate().toString().padStart(2, '0');
        var month = dob.toLocaleString('default', { month: 'short' });
        var year = dob.getFullYear();
        var dobString = day + '-' + month + '-' + year;

        modal.find('#Name').text(data.Name);
        modal.find('#Email').text(data.Email);
        modal.find('#DOB').text(dobString);
        modal.find('#Gender').text(data.Gender === 0 ? 'Male' : 'Female');
        modal.find('#Designation').text(data.Designation);
        modal.find('#State').text(data.State);
        modal.find('#City').text(data.City);
        modal.find('#Postcode').text(data.Postcode);
        modal.find('#Phone').text(data.PhoneNumber);
        modal.find('#Department').text(data.Department);
        modal.find('#MonthlySalary').text("$"+data.MonthlySalary);
        modal.find('#DateOfJoining').text(data.DateOfJoining);
        modal.find('#TotalExperience').text(data.TotalExperience);
        modal.find('#Remark').text(data.Remarks);

        modal.modal('show');
      });
    },
    error: function () {
      Swal.fire({
        icon: 'error',
        title: 'No Records Found',
        text: 'Insert Employee to Display Record',
      });
    }

  });
});

$('.close').on('click', function () {
  $('#EmployeeDetailsModal').modal('hide');
});

function removeclass() {
  $("#eid").removeClass("sorting_disabled");
  $("#eid").removeClass("sorting_asc");
}

