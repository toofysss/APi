<?php
include '../conn.php';
if (isset($_POST['id'])) {
    if ($conn) {
        $domin =  filterRequest('id');
        $query = "SELECT admin.name as name ,admin.email as email ,admin.Phone as phone , dominid.domin as domin from admin  inner join dominid on dominid.id =admin.domin where dominid.id=?";
        $queryparams = array($domin);
        $stmt = sqlsrv_query($conn, $query, $queryparams);
        $result = array();
        while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
            $result[] = $row;
        }
        echo json_encode(array("status" => "success", "data" => $result));
    } else {
        echo "Connection could not be established.<br />";
        die(print_r(sqlsrv_errors(), true));
    }
}
