<?php
include '../conn.php';
if (isset($_POST['domin'])) {
    if ($conn) {
        $domin =  filterRequest('domin');
        $query = "SELECT origencountry.id as id, origencountry.name as name FROM origencountry INNER JOIN dominid ON origencountry.domin = dominid.id WHERE dominid.domin = ? ORDER BY origencountry.id DESC";
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
