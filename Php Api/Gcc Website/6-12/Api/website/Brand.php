<?php
include '../conn.php';
if ($conn) {
    $domin = filterRequest('domin');
    $query = "SELECT brand.id as id, brand.name as name, brand.countryId as coid, origencountry.name as coname FROM brand INNER JOIN origencountry ON brand.countryId = origencountry.id";
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

