$(document).ready(function () {
  var table = null;

  $.ajax({
    url: 'EmployeeData_TodayDate.json',
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
            orderable: false
          },
          {
            data: 'Name'
          },
          {
            data: 'Department',
            render: function (data, type, row) {
              switch (data) {
                case 0:
                  return '<span style="color:red">Sales</span>';
                case 1:
                  return '<span style="color:green">Marketing</span>';
                case 2:
                  return '<span style="color:black">Development</span>';
                case 3:
                  return '<span style="color:blue">QA</span>';
                case 4:
                  return '<span style="color:orange">HR</span>';
                case 5:
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

      // Event listener for the SeeDetails button
      $('#EmployeeTable tbody').on('click', '.SeeDetails', function () {
        var data = table.row($(this).parents('tr')).data();
        var modal = $('#EmployeeDetailsModal');

        modal.find('#Name').text(data.Name);
        modal.find('#Email').text(data.Email);
        modal.find('#DOB').text(data.DOB);
        modal.find('#Gender').text(data.Gender === 0 ? 'Male' : 'Female');
        modal.find('#Designation').text(data.Designation);
        modal.find('#State').text(data.State);
        modal.find('#City').text(data.City);
        modal.find('#Postcode').text(data.Postcode);
        modal.find('#Phone').text(data.PhoneNumber);
        modal.find('#Department').text(data.Department);
        modal.find('#MonthlySalary').text(data.MonthlySalary);
        modal.find('#DateOfJoining').text(data.DateOfJoining);
        modal.find('#TotalExperience').text(data.TotalExperience);
        modal.find('#Remark').text(data.Remarks);

        modal.modal('show');
      });
    }
  });
});

// Add a click event listener to the "x" button
$('.close').on('click', function() {
  // Get the modal element and hide it
  $('#EmployeeDetailsModal').modal('hide');
});
