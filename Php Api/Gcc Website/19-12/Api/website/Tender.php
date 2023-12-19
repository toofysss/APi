<?php
include '../conn.php';


if (isset($_POST['domin']) && isset($_POST['date'])) {
    if ($conn) {
        $date = filterRequest('date');
        $currentDate = date("Y-m-d");
        $sql = "";

        if ($date == 0) {
            $sql .= " AND T_Tenders.ExpiryDate >= ?";
        } else {
            $sql .= " AND T_Tenders.ExpiryDate < ?";
        }

        $params = array($currentDate);

        if (isset($_POST['TenderDscrp']) || isset($_POST['Tendername']) || isset($_POST['specialization'])) {
            if (!empty($_POST['TenderDscrp'])) {
                $sql .= " AND T_Tenders.Dscrp LIKE ?";
                $params[] = '%' . filterRequest('TenderDscrp') . '%';
            }
            if (!empty($_POST['Tendername'])) {
                $sql .= " AND system.id = ?";
                $params[] = filterRequest('Tendername');
            }
            if (!empty($_POST['specialization'])) {
                $sql .= " AND T_Tenders.SpecializationID = ?";
                $params[] = filterRequest('specialization');
            }
        }
        $query = "SELECT (SELECT COUNT(*) FROM T_Tenders WHERE T_Tenders.Dscrp IS NOT NULL AND T_Tenders.State=1) AS table_length,
                    T_Tenders.id as id,  T_Tenders.Dscrp as TendersDscrp, T_Tenders.ExpiryDate as ExpiryDate, T_Tenders.CreationDate as CreationDate, P_Specialization.Specialization as Specialization, 
                    T_Tenders.State as TendersState, T_Tenders.SpecializationID as SpecializationID, system.name as name_ar, system.name_eng as name_eng, system.file_ as logo, T_Tenders.ImgURL as ImgURL
                    FROM T_Tenders
                    INNER JOIN system ON system.domin = T_Tenders.Organizer
                    INNER JOIN P_Specialization ON P_Specialization.id = T_Tenders.SpecializationID
                    WHERE T_Tenders.State = 1 $sql";
        $stmt = sqlsrv_prepare($conn, $query, $params);
        if (sqlsrv_execute($stmt)) {
            $result = array();
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $result[] = $row;
            }
            echo json_encode(array("status" => "success", "data" => $result));
        } else {
            echo "Error executing the query.";
        }
    }

} else if (isset($_POST['domin']) && isset($_POST['id'])) {
    $id = filterRequest('id');

    $query = "SELECT T_Tenders.id as id, T_Tenders.Dscrp as TendersDscrp, T_Tenders.TendersDetails as TendersDetails, T_Tenders.CreationDate as CreationDate, 
                T_Tenders.ExpiryDate as ExpiryDate, P_Specialization.Specialization as Specialization, T_Tenders.SpecializationID as SpecializationID, T_Tenders_Details.ItemDscrp as ItemDscrp,
                T_Tenders_Details.RequiredQuantity as RequiredQuantity, T_Tenders_Details.Notes as Notes, T_TenderConditions.Dscrp as TenderConditionsDscrp , P_TenderUnits.Dscrp as UnitsDscrp,
                T_Tenders_Details.UnitID as UnitID , T_Tenders_Details.id as idDetails ,T_TenderConditions.id as idConditions ,T_Tenders.ImgURL as ImgURL
                FROM T_Tenders
                INNER JOIN P_Specialization ON P_Specialization.id = T_Tenders.SpecializationID 
                INNER JOIN T_Tenders_Details ON T_Tenders_Details.TenderID = T_Tenders.id 
                INNER JOIN T_TenderConditions ON T_TenderConditions.TenderID = T_Tenders.id 
                INNER JOIN P_TenderUnits ON P_TenderUnits.id = T_Tenders_Details.UnitID where T_Tenders.id=?";
    $queryConditions = "SELECT Dscrp FROM T_TenderConditions WHERE T_TenderConditions.TenderID =?";
    $queryparams = array($id);
    $stmt = sqlsrv_query($conn, $query, $queryparams);
    $result = array();
    $queryparamsConditions = array($id);
    $stmtConditions = sqlsrv_query($conn, $queryConditions, $queryparamsConditions);
    $resultConditions = array();

    while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
        // Use the 'id' field as the key to ensure uniqueness
        $result[$row['ItemDscrp']] = $row;
    }
    while ($Conditions = sqlsrv_fetch_array($stmtConditions, SQLSRV_FETCH_ASSOC)) {
        // Use the 'id' field as the key to ensure uniqueness
        $resultConditions[] = $Conditions;
    }

    // Convert the associative array into a simple indexed array
    $result = array_values($result);
    $resultConditions = array_values($resultConditions);

    echo json_encode(array("status" => "success", "data" => $result, "Condation" => $resultConditions));

}