<?php

include "../conn.php";
if (isset($_POST['domin'])) {
    $domin = filterRequest('domin');
    if ($conn) {
        if ($domin == '0') {
            $query = "SELECT system.domin as id, system.name as sysname, system.name_eng as engname ,system.domin as domin  FROM system Where system.Conferences=1";
        } else {
            $query = "SELECT system.domin as id, system.name as sysname,system.name_eng as engname ,system.domin as domin  FROM system
                        INNER JOIN dominid ON system.domin = dominid.id WHERE dominid.domin = ? and system.Conferences=1";
        }
        $queryparams = array($domin);
        $stmt = sqlsrv_query($conn, $query, $queryparams);
        $result = array();
        while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
            $result[] = $row;
        }
        echo json_encode(array("status" => "success", "data" => $result));
    }
}