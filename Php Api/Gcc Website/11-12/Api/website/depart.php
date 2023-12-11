<?php
include '../conn.php';

if (isset($_POST['domin']) && isset($_POST['count'])) {
    if ($conn) {
        $domin =  filterRequest('domin');
        $count = filterRequest('count');
        if ($count == 0) {
            $query = "SELECT * FROM depart
                    INNER JOIN dominid ON depart.domin = dominid.id
                    WHERE dominid.domin = ? AND depart.servicesID IS NOT NULL
                    ORDER BY depart.id DESC ";
            $queryparams = array($domin);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        } else {
            $query = "SELECT * FROM depart
                    INNER JOIN dominid ON depart.domin = dominid.id
                    WHERE dominid.domin = ? AND depart.servicesID IS NOT NULL
                    ORDER BY depart.id DESC OFFSET 0 ROWS FETCH NEXT $count ROWS ONLY";
            $queryparams = array($domin);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        }
    } else {
        echo "Connection could not be established.<br/>";
        die(print_r(sqlsrv_errors(), true));
    }
}
