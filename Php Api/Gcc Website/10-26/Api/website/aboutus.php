<?php
include '../conn.php';
if (isset($_POST['domin'])) {
    if ($conn) {
        $domin =  filterRequest('domin');
        if ($domin == 0) {
            $server = "gcc.iq";
            // $server = "localhost";
            $query = "SELECT *,contact.C_description FROM system INNER JOIN dominid ON system.domin = dominid.id
                        INNER JOIN contact ON contact.domin = dominid.id
                        WHERE dominid.domin = ?";
            $queryparams = array($server);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();

            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        } else {
            $query = "SELECT *,contact.C_description FROM system INNER JOIN dominid ON system.domin = dominid.id
                        INNER JOIN contact ON contact.domin = dominid.id
                        WHERE dominid.domin = ?";
            $queryparams = array($domin);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        }
    } else {
        echo "Connection could not be established.<br />";
        die(print_r(sqlsrv_errors(), true));
    }
}
