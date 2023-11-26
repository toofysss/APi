<?php
include '../conn.php';
if (isset($_POST['domin'])) {
    $domin =  filterRequest('domin');
    if ($domin == 0) {
        if ($conn) {
         $server = "gcc.iq";
            // $server = "localhost";
            $query = "SELECT *,system.file_ FROM contact 
            INNER JOIN dominid ON contact.domin = dominid.id 
            INNER JOIN system ON system.domin = dominid.id
            WHERE dominid.domin = ?";
            $queryparams = array($server);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        } else {
            echo json_encode(array("status" => "fail", "data" => $result));
        }
    } else {
        if ($conn) {
            $query = "SELECT *,system.file_ FROM contact 
            INNER JOIN dominid ON contact.domin = dominid.id 
            INNER JOIN system ON system.domin = dominid.id
            WHERE dominid.domin =?";
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
} 
