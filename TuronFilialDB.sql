-- MySQL dump 10.13  Distrib 8.0.25, for Win64 (x86_64)
--
-- Host: localhost    Database: turonfilial
-- ------------------------------------------------------
-- Server version	8.0.25

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `cart`
--

DROP TABLE IF EXISTS `cart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cart` (
  `id` int NOT NULL AUTO_INCREMENT,
  `shop_id` int NOT NULL,
  `product_id` int NOT NULL,
  `name` varchar(100) NOT NULL,
  `price` double NOT NULL,
  `val_id` int NOT NULL,
  `quantity` double NOT NULL,
  `total` double NOT NULL,
  PRIMARY KEY (`id`),
  KEY `shop_id` (`shop_id`),
  CONSTRAINT `cart_ibfk_1` FOREIGN KEY (`shop_id`) REFERENCES `shop` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=320 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cart`
--

LOCK TABLES `cart` WRITE;
/*!40000 ALTER TABLE `cart` DISABLE KEYS */;
INSERT INTO `cart` VALUES (1,1,1,'Qozoq un',2000,1,50,100000),(319,2,2,'Andijon un',1500,1,50,75000);
/*!40000 ALTER TABLE `cart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `changedprice`
--

DROP TABLE IF EXISTS `changedprice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `changedprice` (
  `id` int NOT NULL AUTO_INCREMENT,
  `date` date NOT NULL,
  `difference_som` double NOT NULL,
  `difference_dollar` double NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `changedprice`
--

LOCK TABLES `changedprice` WRITE;
/*!40000 ALTER TABLE `changedprice` DISABLE KEYS */;
INSERT INTO `changedprice` VALUES (1,'2021-08-22',-990000,0);
/*!40000 ALTER TABLE `changedprice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `chek`
--

DROP TABLE IF EXISTS `chek`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `chek` (
  `id` int NOT NULL AUTO_INCREMENT,
  `header` varchar(500) NOT NULL,
  `footer` varchar(500) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `chek`
--

LOCK TABLES `chek` WRITE;
/*!40000 ALTER TABLE `chek` DISABLE KEYS */;
INSERT INTO `chek` VALUES (1,'Turon unlari','Xaridingiz uchun tashakkur\r\ndoimiy mijozimiz bolib qoling');
/*!40000 ALTER TABLE `chek` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `debt`
--

DROP TABLE IF EXISTS `debt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `debt` (
  `id` int NOT NULL AUTO_INCREMENT,
  `debtor_id` int NOT NULL,
  `shop_id` int NOT NULL,
  `return_date` date NOT NULL,
  PRIMARY KEY (`id`),
  KEY `debtor_id` (`debtor_id`),
  KEY `shop_id` (`shop_id`),
  CONSTRAINT `debt_ibfk_1` FOREIGN KEY (`debtor_id`) REFERENCES `debtor` (`id`),
  CONSTRAINT `debt_ibfk_2` FOREIGN KEY (`shop_id`) REFERENCES `shop` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `debt`
--

LOCK TABLES `debt` WRITE;
/*!40000 ALTER TABLE `debt` DISABLE KEYS */;
INSERT INTO `debt` VALUES (1,2,2,'2021-08-31');
/*!40000 ALTER TABLE `debt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `debtor`
--

DROP TABLE IF EXISTS `debtor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `debtor` (
  `id` int NOT NULL AUTO_INCREMENT,
  `mijoz_fish` varchar(100) NOT NULL,
  `tel_1` char(20) NOT NULL,
  `tel_2` char(20) DEFAULT NULL,
  `qarz_som` double NOT NULL,
  `qarz_dollar` double NOT NULL,
  `difference` double NOT NULL,
  `status_difference` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `debtor`
--

LOCK TABLES `debtor` WRITE;
/*!40000 ALTER TABLE `debtor` DISABLE KEYS */;
INSERT INTO `debtor` VALUES (1,'oddiy klient','1','1',0,0,0,0),(2,'Abdulloh','996544849','',9000,0,0,0);
/*!40000 ALTER TABLE `debtor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `filial`
--

DROP TABLE IF EXISTS `filial`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `filial` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `address` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `filial`
--

LOCK TABLES `filial` WRITE;
/*!40000 ALTER TABLE `filial` DISABLE KEYS */;
INSERT INTO `filial` VALUES (1,'Asosiy filial/ombor','Andijon');
/*!40000 ALTER TABLE `filial` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `guruh`
--

DROP TABLE IF EXISTS `guruh`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `guruh` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `guruh`
--

LOCK TABLES `guruh` WRITE;
/*!40000 ALTER TABLE `guruh` DISABLE KEYS */;
INSERT INTO `guruh` VALUES (1,'кор'),(2,'люстра'),(3,'эл товар'),(4,'шит'),(5,'бентонг'),(6,'дусел'),(7,'Лист8'),(8,'шит+стрем'),(9,'веста'),(10,'ойдин'),(11,'гг'),(12,'термо'),(13,'кабел'),(14,'чинт'),(15,'прайм');
/*!40000 ALTER TABLE `guruh` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payhistory`
--

DROP TABLE IF EXISTS `payhistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `payhistory` (
  `id` int NOT NULL AUTO_INCREMENT,
  `debtor_id` int NOT NULL,
  `given_som` double NOT NULL,
  `given_dollar` double NOT NULL,
  `kurs` double NOT NULL,
  `date` datetime NOT NULL,
  `status_server` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `debtor_id` (`debtor_id`),
  CONSTRAINT `payhistory_ibfk_1` FOREIGN KEY (`debtor_id`) REFERENCES `debtor` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payhistory`
--

LOCK TABLES `payhistory` WRITE;
/*!40000 ALTER TABLE `payhistory` DISABLE KEYS */;
INSERT INTO `payhistory` VALUES (1,2,1000,0,10700,'2021-08-22 12:42:38',1);
/*!40000 ALTER TABLE `payhistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pricecart`
--

DROP TABLE IF EXISTS `pricecart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pricecart` (
  `id` int NOT NULL AUTO_INCREMENT,
  `ch_id` int NOT NULL,
  `pr_name` varchar(100) NOT NULL,
  `old_price` double NOT NULL,
  `new_price` double NOT NULL,
  `residue` double NOT NULL,
  `difference` double NOT NULL,
  `total_diff` double NOT NULL,
  `val_id` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pricecart`
--

LOCK TABLES `pricecart` WRITE;
/*!40000 ALTER TABLE `pricecart` DISABLE KEYS */;
INSERT INTO `pricecart` VALUES (1,1,'Qozoq un',2000,1800,4950,-200,-990000,1);
/*!40000 ALTER TABLE `pricecart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `id` int NOT NULL AUTO_INCREMENT,
  `product_id` int NOT NULL,
  `name` varchar(100) NOT NULL,
  `t_price` double NOT NULL,
  `price` double NOT NULL,
  `val_id` int NOT NULL,
  `quantity` double NOT NULL,
  `barcode` char(30) NOT NULL,
  `gurux` int NOT NULL,
  `measurement` varchar(10) NOT NULL,
  `preparer` varchar(40) NOT NULL,
  `min_count` double NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2846 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,1,'Qozoq un',1500,1800,1,4960,'1000',1,'kg','Turon',100),(2845,2,'Andijon un',1200,1500,1,4900,'1001',1,'kg','Turon',500);
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retdelproduct`
--

DROP TABLE IF EXISTS `retdelproduct`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `retdelproduct` (
  `id` int NOT NULL AUTO_INCREMENT,
  `retsum_id` int NOT NULL,
  `pr_name` varchar(50) NOT NULL,
  `preparer` varchar(50) NOT NULL,
  `measurement` varchar(10) NOT NULL,
  `tan_narx` double NOT NULL,
  `sotish_narx` double NOT NULL,
  `val_id` int NOT NULL,
  `barcode` varchar(30) NOT NULL,
  `qayt_miqdor` double NOT NULL,
  PRIMARY KEY (`id`),
  KEY `retsum_id` (`retsum_id`),
  CONSTRAINT `retdelproduct_ibfk_1` FOREIGN KEY (`retsum_id`) REFERENCES `retdelsumma` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retdelproduct`
--

LOCK TABLES `retdelproduct` WRITE;
/*!40000 ALTER TABLE `retdelproduct` DISABLE KEYS */;
INSERT INTO `retdelproduct` VALUES (1,1,'Andijon un','Turon','uz',1200,1500,1,'1001',50);
/*!40000 ALTER TABLE `retdelproduct` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `retdelsumma`
--

DROP TABLE IF EXISTS `retdelsumma`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `retdelsumma` (
  `id` int NOT NULL AUTO_INCREMENT,
  `deliver_id` int NOT NULL,
  `deliver` varchar(50) NOT NULL,
  `date` date NOT NULL,
  `som` double NOT NULL,
  `dollar` double NOT NULL,
  `kurs` double NOT NULL,
  `status` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `retdelsumma`
--

LOCK TABLES `retdelsumma` WRITE;
/*!40000 ALTER TABLE `retdelsumma` DISABLE KEYS */;
INSERT INTO `retdelsumma` VALUES (1,1,'Yetkazib beruvchi 1','2021-08-22',60000,0,10700,1);
/*!40000 ALTER TABLE `retdelsumma` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `returnproduct`
--

DROP TABLE IF EXISTS `returnproduct`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `returnproduct` (
  `id` int NOT NULL AUTO_INCREMENT,
  `shop_id` int NOT NULL,
  `product_id` int NOT NULL,
  `return_quantity` double NOT NULL,
  `summa` double NOT NULL,
  `val_id` int NOT NULL,
  `sold` int NOT NULL,
  `debt` int NOT NULL,
  `date` datetime NOT NULL,
  `difference` double NOT NULL,
  `status_server` int NOT NULL,
  `barcode` char(30) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `shop_id` (`shop_id`),
  CONSTRAINT `returnproduct_ibfk_1` FOREIGN KEY (`shop_id`) REFERENCES `shop` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `returnproduct`
--

LOCK TABLES `returnproduct` WRITE;
/*!40000 ALTER TABLE `returnproduct` DISABLE KEYS */;
INSERT INTO `returnproduct` VALUES (1,1,1,10,20000,1,1,0,'2021-08-22 12:44:29',-2000,0,'1000');
/*!40000 ALTER TABLE `returnproduct` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `send`
--

DROP TABLE IF EXISTS `send`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `send` (
  `id` int NOT NULL,
  `password` char(16) NOT NULL,
  `first_name` varchar(30) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `send`
--

LOCK TABLES `send` WRITE;
/*!40000 ALTER TABLE `send` DISABLE KEYS */;
INSERT INTO `send` VALUES (1,'123','Turon');
/*!40000 ALTER TABLE `send` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shop`
--

DROP TABLE IF EXISTS `shop`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shop` (
  `id` int NOT NULL AUTO_INCREMENT,
  `naqd` double NOT NULL,
  `plastik` double NOT NULL,
  `nasiya_som` double NOT NULL,
  `nasiya_dollar` double NOT NULL,
  `transfer` double NOT NULL,
  `currency` double NOT NULL,
  `total_som` double NOT NULL,
  `total_dollar` double NOT NULL,
  `kurs` double NOT NULL,
  `date` datetime NOT NULL,
  `status_tulov` int NOT NULL,
  `queue` int NOT NULL,
  `debt` int NOT NULL,
  `status_server` int NOT NULL,
  `difference_som` double NOT NULL,
  `difference_dollar` double NOT NULL,
  `sellar_id` int NOT NULL,
  `debtor_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `sellar_id` (`sellar_id`),
  CONSTRAINT `shop_ibfk_1` FOREIGN KEY (`sellar_id`) REFERENCES `userprofile` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=122 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shop`
--

LOCK TABLES `shop` WRITE;
/*!40000 ALTER TABLE `shop` DISABLE KEYS */;
INSERT INTO `shop` VALUES (1,0,0,0,0,0,9,100000,0,10700,'2021-08-22 10:43:43',1,0,0,1,3700,0,1,1),(2,11500,0,10000,0,0,5,75000,0,10700,'2021-08-22 11:11:55',1,0,1,1,0,0,1,2);
/*!40000 ALTER TABLE `shop` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shopid`
--

DROP TABLE IF EXISTS `shopid`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shopid` (
  `id` int NOT NULL AUTO_INCREMENT,
  `shop_id` int NOT NULL,
  `mac_address` char(50) NOT NULL,
  `password` char(16) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shopid`
--

LOCK TABLES `shopid` WRITE;
/*!40000 ALTER TABLE `shopid` DISABLE KEYS */;
INSERT INTO `shopid` VALUES (1,0,'E4A8DFF62AAC','123');
/*!40000 ALTER TABLE `shopid` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `staff`
--

DROP TABLE IF EXISTS `staff`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `staff` (
  `id` int NOT NULL AUTO_INCREMENT,
  `staff` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `staff`
--

LOCK TABLES `staff` WRITE;
/*!40000 ALTER TABLE `staff` DISABLE KEYS */;
INSERT INTO `staff` VALUES (1,'Filial ish boshqaruvchisi'),(2,'Filial sotuvchisi'),(3,'Kassir');
/*!40000 ALTER TABLE `staff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userprofile`
--

DROP TABLE IF EXISTS `userprofile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userprofile` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL DEFAULT 'Jayhunelectro',
  `password` char(16) NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `staff_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `staff_id` (`staff_id`),
  CONSTRAINT `userprofile_ibfk_1` FOREIGN KEY (`staff_id`) REFERENCES `staff` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userprofile`
--

LOCK TABLES `userprofile` WRITE;
/*!40000 ALTER TABLE `userprofile` DISABLE KEYS */;
INSERT INTO `userprofile` VALUES (1,'turonfilial','123','Turon','Unlari',1),(2,'Jayhunelectro','123456','Umidjon','Soliyev',2);
/*!40000 ALTER TABLE `userprofile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `val`
--

DROP TABLE IF EXISTS `val`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `val` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(30) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `val`
--

LOCK TABLES `val` WRITE;
/*!40000 ALTER TABLE `val` DISABLE KEYS */;
INSERT INTO `val` VALUES (1,'uz'),(2,'$');
/*!40000 ALTER TABLE `val` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `valyuta`
--

DROP TABLE IF EXISTS `valyuta`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `valyuta` (
  `id` int NOT NULL AUTO_INCREMENT,
  `kurs` double NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `valyuta`
--

LOCK TABLES `valyuta` WRITE;
/*!40000 ALTER TABLE `valyuta` DISABLE KEYS */;
INSERT INTO `valyuta` VALUES (1,10700);
/*!40000 ALTER TABLE `valyuta` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-08-22 13:02:04
