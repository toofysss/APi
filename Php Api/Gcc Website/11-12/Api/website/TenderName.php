<?php
include "../conn.php";

if ($conn) {
    $query = "SELECT system.id as id, system.name as name_ar, system.name_eng as engname ,system.domin as domin FROM system Where system.Tender=1";
    $stmt = sqlsrv_query($conn, $query);
    $result = array();
    while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
        $result[] = $row;
    }
    echo json_encode(array("status" => "success", "data" => $result));
} else {
    echo "Connection could not be established.<br />";
    die(print_r(sqlsrv_errors(), true));
}