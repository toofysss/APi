<?php
include '../conn.php';
if (isset($_POST['domin']) && isset($_POST['date'])) {
    if ($conn) {
        $domin = filterRequest('domin');
        $date = filterRequest('date');
        $currentDate = date("Y-m-d");
        $sql = "";
        if ($date == 0) {
            $sql .= "AND T_Conference.ConfLastDate >= '$currentDate'";
        } else {
            $sql .= "AND T_Conference.ConfLastDate < '$currentDate'";
        }
        if (isset($_POST['sysname']) || isset($_POST['DefinitionForm']) || isset($_POST['specialization']) || isset($_POST['country']) || isset($_POST['start_date']) || isset($_POST[''])) {
            if (!empty($_POST['sysname'])) {
                $sql .= " AND T_Conference.domin = " . filterRequest('sysname');
            }
            if (!empty($_POST['specialization'])) {
                $sql .= " AND T_Conference.ConfDepartment = " . filterRequest('specialization');
            }
            if (!empty($_POST['country'])) {
                $sql .= " AND T_Conference.ConfCountry =" . filterRequest('country');
            }
            if (!empty($_POST['start_date'])) {
                $sql .= " AND T_Conference.ConfFromDate >= '" . filterRequest('start_date') . "'";
            }
            if (!empty($_POST['end_date'])) {
                $sql .= " AND T_Conference.ConfToDate <= '" . filterRequest('end_date') . "'";
            }
            if (!empty($_POST['DefinitionForm'])) {
                $sql .= " AND T_Conference.DefinitionFormID = '" . filterRequest('DefinitionForm') . "'";
            }
        }
        if ($domin == 0) {
            $query = "SELECT (SELECT COUNT(*) FROM T_Conference WHERE T_Conference.ConfName IS NOT NULL AND T_Conference.ConfDepartment IS NOT NULL AND T_Conference.ConfCountry IS NOT NULL AND T_Conference.ConfCity IS NOT NULL  $sql) AS table_length,
                        system.name as sysname ,system.name_eng as engname , system.file_ as imagefile,
                        T_Conference.id AS id, T_Conference.ConfName AS ConfName, P_Country.Country AS Country, DefinitionForm.Dscrp AS DefinitionForm,
                        P_City.City AS City, P_Specialization.Specialization AS Specialization, T_Conference.ConfFromDate AS ConfFromDate, 
                        T_Conference.ConfToDate AS ConfToDate, T_Conference.ConfLastDate AS ConfLastDate, T_Conference.ConfDscrp AS ConfDscrp, 
                        T_Conference.ConfFile AS ConfFile FROM T_Conference
                        INNER JOIN P_Country ON P_Country.id = T_Conference.ConfCountry
                        INNER JOIN P_City ON P_City.id = T_Conference.ConfCity
                        INNER JOIN P_Specialization ON P_Specialization.id = T_Conference.ConfDepartment
                        INNER JOIN DefinitionForm ON DefinitionForm.id = T_Conference.DefinitionFormID
                        INNER JOIN system ON system.domin = T_Conference.domin
                        WHERE T_Conference.ConfName IS NOT NULL  $sql
                        ORDER BY T_Conference.id";
        } else {
            $query = "SELECT (SELECT COUNT(*) FROM T_Conference 
                        INNER JOIN dominid ON T_Conference.domin = dominid.id 
                        WHERE T_Conference.ConfName IS NOT NULL AND dominid.domin ='$domin' AND T_Conference.ConfDepartment IS NOT NULL 
                        AND T_Conference.ConfCountry IS NOT NULL AND T_Conference.ConfCity IS NOT NULL $sql) AS table_length,
                        system.name as sysname ,system.name_eng as engname , system.file_ as imagefile,
                        T_Conference.id AS id, T_Conference.ConfName AS ConfName, P_Country.Country AS Country, DefinitionForm.Dscrp AS DefinitionForm,
                        P_City.City AS City, P_Specialization.Specialization AS Specialization, T_Conference.ConfFromDate AS ConfFromDate, 
                        T_Conference.ConfToDate AS ConfToDate, T_Conference.ConfLastDate AS ConfLastDate, T_Conference.ConfDscrp AS ConfDscrp, 
                        T_Conference.ConfFile AS ConfFile FROM T_Conference
                        INNER JOIN P_Country ON P_Country.id = T_Conference.ConfCountry
                        INNER JOIN P_City ON P_City.id = T_Conference.ConfCity
                        INNER JOIN P_Specialization ON P_Specialization.id = T_Conference.ConfDepartment
                        INNER JOIN DefinitionForm ON DefinitionForm.id = T_Conference.DefinitionFormID
                        INNER JOIN system ON system.domin = T_Conference.domin
                        INNER JOIN dominid ON T_Conference.domin = dominid.id 
                        WHERE dominid.domin = ? $sql
                        ORDER BY T_Conference.id";
        }
        $queryparams = array($domin);
        $stmt = sqlsrv_query($conn, $query, $queryparams);
        $result = array();
        while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
            $result[] = $row;
        }
        echo json_encode(array("status" => "success", "data" => $result));

    }
} else if (isset($_POST['domin']) && isset($_POST['id'])) {
    $id = filterRequest('id');

    $query = "SELECT T_Conference.id AS id, T_Conference.ConfName AS ConfName, P_Country.Country AS Country, DefinitionForm.Dscrp AS DefinitionForm,
                        system.name as sysname ,system.name_eng as engname , system.file_ as imagefile,
                        P_City.City AS City, P_Specialization.Specialization AS Specialization, T_Conference.ConfFromDate AS ConfFromDate, 
                        T_Conference.ConfToDate AS ConfToDate, T_Conference.ConfLastDate AS ConfLastDate, T_Conference.ConfDscrp AS ConfDscrp, 
                        T_Conference.ConfFile AS ConfFile FROM T_Conference
                        INNER JOIN P_Country ON P_Country.id = T_Conference.ConfCountry
                        INNER JOIN P_City ON P_City.id = T_Conference.ConfCity
                        INNER JOIN P_Specialization ON P_Specialization.id = T_Conference.ConfDepartment
                        INNER JOIN DefinitionForm ON DefinitionForm.id = T_Conference.DefinitionFormID
                        INNER JOIN system ON system.domin = T_Conference.domin
                        INNER JOIN dominid ON T_Conference.domin = dominid.id 
                        WHERE T_Conference.id = ?
                        ORDER BY T_Conference.id";

    $queryparams = array($id);
    $stmt = sqlsrv_query($conn, $query, $queryparams);
    $result = array();
    while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
        $result[] = $row;
    }
    echo json_encode(array("status" => "success", "data" => $result));

}