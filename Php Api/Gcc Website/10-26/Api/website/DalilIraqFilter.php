

<?php
include "../conn.php";
$DalilIraqID = filterRequest('DalilIraqID');

if (isset($_POST['DalilIraqID'])) {
    if (isset($_POST['check'])) {
        $check = filterRequest('check');

        if ($check == 0) {

            if ($conn) {
                $query = "SELECT dominid.id AS dominid_id, dominid.domin AS dominid_domin,system.background as bg,system.name_eng as system_name_en, system.name AS system_name, system.file_ AS system_file, system.active AS system_active, system.iraq_guide_id as iraq_guide_id, system.domin AS system_domin, admin.email AS admin_email, admin.password AS admin_password FROM dominid
                JOIN system ON dominid.id = system.domin
                JOIN admin ON system.domin = admin.domin
                INNER JOIN IraqGuide ON system.iraq_guide_id = IraqGuide.id
                where system.iraq_guide_id=? ORDER BY dominid.id";
                $queryparams = array($DalilIraqID);
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
        } else if ($check == 1) {
            $Data = filterRequest('data');
            if ($conn) {
                $query = "SELECT dominid.id AS dominid_id, dominid.domin AS dominid_domin,system.background as bg, system.name AS system_name, system.file_ AS system_file, system.active AS system_active, system.iraq_guide_id as iraq_guide_id, system.domin AS system_domin, admin.email AS admin_email, admin.password AS admin_password FROM dominid
                JOIN system ON dominid.id = system.domin
                JOIN admin ON system.domin = admin.domin
                INNER JOIN IraqGuide ON system.iraq_guide_id = IraqGuide.id
                where system.iraq_guide_id=? and  system.name like '%$Data%' ORDER BY dominid.id";
                $queryparams = array($DalilIraqID);
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
        } else if ($check == 2) {
            $Data = filterRequest('data');
            if ($conn) {
                $query = "SELECT dominid.id AS dominid_id, dominid.domin AS dominid_domin,system.background as bg, system.name AS system_name, system.file_ AS system_file, system.active AS system_active, system.iraq_guide_id as iraq_guide_id, system.domin AS system_domin, admin.email AS admin_email, admin.password AS admin_password FROM dominid
                JOIN system ON dominid.id = system.domin
                JOIN admin ON system.domin = admin.domin
                INNER JOIN IraqGuide ON system.iraq_guide_id = IraqGuide.id
                where system.iraq_guide_id=? and  system.active like '%$Data%' ORDER BY dominid.id";
                $queryparams = array($DalilIraqID);
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
}
