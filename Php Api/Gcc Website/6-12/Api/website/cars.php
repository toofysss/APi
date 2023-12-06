<?php
include '../conn.php';

if (isset($_POST['domin']) && isset($_POST['count']) && isset($_POST['solid'])) {
    if ($conn) {
        $domin = filterRequest('domin');
        $count = filterRequest('count');
        $solid = filterRequest('solid');
        if ($count == 0) {
            $query = "SELECT Cars_T_Details.CarTitle as CarTitle, Cars_T_Details.id as id, Cars_T_Details.ManufactureDate as ManufactureDate, Car_Vehicle_Type.Dscrp as type, Car_vehicleBrands.Dscrp as brand, Car_Currencies.shortcurrencies as currencies, Cars_T_Details.PriceAmount as PriceAmount 
            FROM Cars_T_Details
            INNER JOIN Car_Vehicle_Type ON Car_Vehicle_Type.id = Cars_T_Details.CarClassID
            INNER JOIN Car_vehicleBrands ON Car_vehicleBrands.id = Cars_T_Details.BrandID
            INNER JOIN Car_Currencies ON Car_Currencies.id = Cars_T_Details.CurrencyID
            INNER JOIN dominid ON Cars_T_Details.domin = dominid.id 
            WHERE dominid.domin = ? AND Cars_T_Details.IsSold = ?
            ORDER BY Cars_T_Details.id DESC";
            
            $image = "SELECT * FROM T_CarImage WHERE CarID IN (SELECT Cars_T_Details.id FROM Cars_T_Details 
            INNER JOIN dominid ON Cars_T_Details.domin = dominid.id 
            WHERE dominid.domin = ? AND IsSold = ?)";
            $queryparams = array($domin, $solid);
            $queryparamsimage = array($domin, $solid);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $stmtimage = sqlsrv_query($conn, $image, $queryparamsimage);
            $result = array();
            $carImages = array();
            while ($stmtimagerow = sqlsrv_fetch_array($stmtimage, SQLSRV_FETCH_ASSOC)) {
                $carID = $stmtimagerow["CarID"];
                $imagePath = $stmtimagerow["Dscrp"];
                if (!isset($carImages[$carID])) {
                    $carImages[$carID] = array();
                }
                $carImages[$carID][] = $imagePath;
            }
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $carID = $row["id"];
                $carData = array(
                    "car_id" => $carID,
                    "car" => array(
                        "carname" => $row["CarTitle"],
                        "ManufactureDate" => $row["ManufactureDate"],
                        "CarType" => $row["type"],
                        "brand" => $row["brand"],
                        "price" => $row["PriceAmount"],
                        "currency" => $row["currencies"],
                        "imagepath" => isset($carImages[$carID]) ? $carImages[$carID] : []
                    )
                );
                $result[] = $carData;
            }

            $response = array("cars" => $result);
            echo json_encode($response, JSON_UNESCAPED_UNICODE);

        } else {
            $query = "SELECT TOP  $count Cars_T_Details.CarTitle as CarTitle, Cars_T_Details.id as id, Cars_T_Details.ManufactureDate as ManufactureDate, Car_Vehicle_Type.Dscrp as type, Car_vehicleBrands.Dscrp as brand, Car_Currencies.shortcurrencies as currencies, Cars_T_Details.PriceAmount as PriceAmount 
            FROM Cars_T_Details
            INNER JOIN Car_Vehicle_Type ON Car_Vehicle_Type.id = Cars_T_Details.CarClassID
            INNER JOIN Car_vehicleBrands ON Car_vehicleBrands.id = Cars_T_Details.BrandID
            INNER JOIN Car_Currencies ON Car_Currencies.id = Cars_T_Details.CurrencyID
            INNER JOIN dominid ON Cars_T_Details.domin = dominid.id 
            WHERE dominid.domin = ? AND Cars_T_Details.IsSold = ?
            ORDER BY Cars_T_Details.id DESC";

            $image = "SELECT * FROM T_CarImage WHERE CarID IN (SELECT Cars_T_Details.id FROM Cars_T_Details 
            INNER JOIN dominid ON Cars_T_Details.domin = dominid.id 
            WHERE dominid.domin = ? AND IsSold = ?)";
            $queryparams = array($domin, $solid);
            $queryparamsimage = array($domin, $solid);
            $stmt = sqlsrv_query($conn, $query, $queryparams);
            $stmtimage = sqlsrv_query($conn, $image, $queryparamsimage);
            $result = array();
            $carImages = array();
            while ($stmtimagerow = sqlsrv_fetch_array($stmtimage, SQLSRV_FETCH_ASSOC)) {
                $carID = $stmtimagerow["CarID"];
                $imagePath = $stmtimagerow["Dscrp"];
                if (!isset($carImages[$carID])) {
                    $carImages[$carID] = array();
                }
                $carImages[$carID][] = $imagePath;
            }
            while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                $carID = $row["id"];
                $carData = array(
                    "car_id" => $carID,
                    "car" => array(
                        "carname" => $row["CarTitle"],
                        "ManufactureDate" => $row["ManufactureDate"],
                        "CarType" => $row["type"],
                        "brand" => $row["brand"],
                        "price" => $row["PriceAmount"],
                        "currency" => $row["currencies"],
                        "imagepath" => isset($carImages[$carID]) ? $carImages[$carID] : []
                    )
                );
                $result[] = $carData;
            }

            $response = array("cars" => $result);
            echo json_encode($response, JSON_UNESCAPED_UNICODE);

        }
    } else {
        echo "Connection could not be established.<br/>";
        die(print_r(sqlsrv_errors(), true));
    }
} else if (isset($_POST['domin']) && isset($_POST['id'])) {
    $CarID = filterRequest('id');

    $query = "SELECT Cars_T_Details.CarTitle as CarTitle, Cars_T_Details.EnginSize as EnginSize,Cars_T_Details.DamageTaype as DamageTaype,Cars_T_Details.KM_Running as KM_Running, Cars_T_Details.id as id, Cars_T_Details.ManufactureDate as ManufactureDate,Cars_T_Details.ShaseNumber as ShaseNumber, Cars_T_Details.Specifications as Specifications, Cars_T_Details.PriceAmount as PriceAmount, 
                Car_Vehicle_Type.Dscrp as DscrpType,Cars_P_SwitchType.Dscrp as DscrpSwitchType,Cars_p_Geartype.Dscrp as DscrpGeartype,Cars_P_Countertype.Dscrp as DscrpCountertype, Car_vehicleBrands.Dscrp as brand, Car_vehicles__State.Dscrp as StateCar,
                Car_Currencies.shortcurrencies as currencies, CarColors.Dscrp as DscrpColor,P_Country.Country as DscrpCountry,gp_Governorate.Dscrp as DscrpGovernorate ,Cars_P_Fueltype.Dscrp as DscrpFueltype,
                contact.WhatsApp ,contact.phone 
				FROM Cars_T_Details
                INNER JOIN Cars_P_SwitchType ON Cars_P_SwitchType.id = Cars_T_Details.SwitchTypeID   
                INNER JOIN Cars_p_Geartype ON Cars_p_Geartype.id = Cars_T_Details.Geartype   
                INNER JOIN Cars_P_Countertype ON Cars_P_Countertype.id = Cars_T_Details.CounterType   
                INNER JOIN Cars_P_Fueltype ON Cars_P_Fueltype.id = Cars_T_Details.FuelType   
                LEFT JOIN gp_Governorate ON gp_Governorate.id = Cars_T_Details.GovernorateID   
                INNER JOIN P_Country ON P_Country.id = Cars_T_Details.CarOrigenID   
                INNER JOIN CarColors ON CarColors.id = Cars_T_Details.ColorID  
				INNER JOIN contact ON contact.domin = Cars_T_Details.domin   
                INNER JOIN Car_vehicles__State ON Car_vehicles__State.id = Cars_T_Details.CarStateID          
                INNER JOIN Car_Vehicle_Type ON Car_Vehicle_Type.id = Cars_T_Details.CarClassID
                INNER JOIN Car_vehicleBrands ON Car_vehicleBrands.id = Cars_T_Details.BrandID
                INNER JOIN Car_Currencies ON Car_Currencies.id = Cars_T_Details.CurrencyID
                WHERE Cars_T_Details.id =?";

    $image = "SELECT * FROM T_CarImage WHERE CarID IN (SELECT Cars_T_Details.id FROM Cars_T_Details 
                INNER JOIN dominid ON Cars_T_Details.domin = dominid.id 
                WHERE Cars_T_Details.id = ?)";
    $queryparams = array($CarID);
    $queryparamsimage = array($CarID);
    $stmt = sqlsrv_query($conn, $query, $queryparams);
    $stmtimage = sqlsrv_query($conn, $image, $queryparamsimage);
    $result = array();
    $carImages = array();
    while ($stmtimagerow = sqlsrv_fetch_array($stmtimage, SQLSRV_FETCH_ASSOC)) {
        $carID = $stmtimagerow["CarID"];
        $imagePath = $stmtimagerow["Dscrp"];
        if (!isset($carImages[$carID])) {
            $carImages[$carID] = array();
        }
        $carImages[$carID][] = $imagePath;
    }
    while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
        $carID = $row["id"];
        $carData = array(
            "car_id" => $carID,
            "car" => array(
                "carname" => $row["CarTitle"],
                "EnginSize" => $row["EnginSize"],
                "DamageTaype" => $row["DamageTaype"],
                "DscrpCountry" => $row["DscrpCountry"],
                "DscrpFueltype" => $row["DscrpFueltype"],
                "DscrpGovernorate" => $row["DscrpGovernorate"],
                "DscrpColor" => $row["DscrpColor"],
                "currencies" => $row["currencies"],
                "brand" => $row["brand"],
                "KM_Running" => $row["KM_Running"],
                "StateCar" => $row["StateCar"],
                "ShaseNumber" => $row["ShaseNumber"],
                "PriceAmount" => $row["PriceAmount"],
                "Specifications" => $row["Specifications"],
                "ManufactureDate" => $row["ManufactureDate"],
                "DscrpGeartype" => $row["DscrpGeartype"],
                "WhatsApp" => $row["WhatsApp"],
                "phone" => $row["phone"],
                "DscrpCountertype" => $row["DscrpCountertype"],
                "DscrpSwitchType" => $row["DscrpSwitchType"],
                "DscrpType" => $row["DscrpType"],

                "imagepath" => isset($carImages[$carID]) ? $carImages[$carID] : []
            )
        );
        $result[] = $carData;
    }

    $response = array("cars" => $result);
    echo json_encode($response, JSON_UNESCAPED_UNICODE);
}
