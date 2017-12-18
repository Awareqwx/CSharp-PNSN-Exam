CREATE DATABASE  IF NOT EXISTS `profnetwork` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `profnetwork`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: profnetwork
-- ------------------------------------------------------
-- Server version	5.7.19-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `connections`
--

DROP TABLE IF EXISTS `connections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `connections` (
  `ConnectionId` int(11) NOT NULL AUTO_INCREMENT,
  `ConnectorId` int(11) DEFAULT NULL,
  `ConnectedToId` int(11) DEFAULT NULL,
  `Accepted` int(11) DEFAULT NULL,
  PRIMARY KEY (`ConnectionId`),
  KEY `fk_Users_has_Users_Users2_idx` (`ConnectedToId`),
  KEY `fk_Users_has_Users_Users1_idx` (`ConnectorId`),
  CONSTRAINT `fk_Users_has_Users_Users1` FOREIGN KEY (`ConnectorId`) REFERENCES `users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Users_has_Users_Users2` FOREIGN KEY (`ConnectedToId`) REFERENCES `users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `connections`
--

LOCK TABLES `connections` WRITE;
/*!40000 ALTER TABLE `connections` DISABLE KEYS */;
INSERT INTO `connections` VALUES (3,2,3,1),(6,4,3,1),(7,1,2,1),(9,4,1,0),(11,2,4,0);
/*!40000 ALTER TABLE `connections` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Email` varchar(45) DEFAULT NULL,
  `PasswordHash` varchar(255) DEFAULT NULL,
  `Description` text,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'2017-12-18 13:13:05','2017-12-18 13:13:05','John Smith','johnsmith@foo.bar','AQAAAAEAACcQAAAAEIJFihh/Wg6/sg6kmAh7t7qRFwvSHjjCtKiqBzh0zu/20zlu74y3pK9AjgORg/wO/g==','I am an extremely generic man, there isn\'t much to say about me that you couldn\'t already intuit.'),(2,'2017-12-18 13:49:34','2017-12-18 13:49:34','Jane Smith','janesmith@foo.bar','AQAAAAEAACcQAAAAEOH7xTW6inLP5hVkl1CxxM0TlNw1mDmXaeiIWH3GV1M6QnO6OqoQ5X0zHKovyi5V8A==','I am an extremely generic woman, there isn\'t much to say about me that you couldn\'t already intuit.'),(3,'2017-12-18 14:09:40','2017-12-18 14:09:40','Will Fletcher','willfletcher@alpha.tck','AQAAAAEAACcQAAAAEGARGtu2JBBbb8YsxmEhx3B6aEckYmMheCrhhERy/I7ZxTWsB5pw+lOLDyuckApteQ==','It is a mystery!'),(4,'2017-12-18 15:08:00','2017-12-18 15:08:00','Ben Peters','benpeters97@yahoo.com','AQAAAAEAACcQAAAAEDcUJUUFKmZ2r9IjYC7i9mHXtrGpQzity3V+lg6kEWZ8qKowW4P20gmAZB2Y5kUMug==','A progrmmer with several years of amateur experience, and a few months of bootcamp.');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-12-18 15:36:21
