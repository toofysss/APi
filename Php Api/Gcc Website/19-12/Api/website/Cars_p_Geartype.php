<?php
include "../conn.php";

if ($conn) {
    $query = "SELECT * FROM Cars_p_Geartype ";
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
