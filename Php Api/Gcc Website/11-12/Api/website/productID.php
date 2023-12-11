<?php
include '../conn.php';
if (isset($_POST['domin'])) {
    if ($conn) {
        $domin =  filterRequest('domin');
        if ($domin == 0) {
            $server = "gcc.iq";
            
            // $server = "localhost";
            $query = "SELECT depart.id as id, depart.name as name, depart.servicesID as seid, depart.imgfile as imgfile, services.name as sename
                        FROM depart INNER JOIN services ON depart.servicesID = services.id INNER JOIN dominid ON depart.domin = dominid.id WHERE dominid.domin = ? $sql ORDER BY depart.id DESC";
            $queryparams = array($server);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();

            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        } else {
            $query = "SELECT depart.id as id, depart.name as name, depart.servicesID as seid, depart.imgfile as imgfile, services.name as sename
            FROM depart INNER JOIN services ON depart.servicesID = services.id INNER JOIN dominid ON depart.domin = dominid.id WHERE dominid.domin = ? $sql ORDER BY depart.id DESC";
            $queryparams = array($domin);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $result = array();
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        }
    }
}
