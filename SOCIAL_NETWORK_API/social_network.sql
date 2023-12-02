-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         10.4.11-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win64
-- HeidiSQL Versión:             12.5.0.6677
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para social_network
CREATE DATABASE IF NOT EXISTS `social_network` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `social_network`;

-- Volcando estructura para tabla social_network.network
CREATE TABLE IF NOT EXISTS `network` (
  `friendship_id` int(11) NOT NULL AUTO_INCREMENT,
  `user1_id` int(11) NOT NULL,
  `user2_id` int(11) NOT NULL,
  `relation_type` varchar(10) NOT NULL DEFAULT 'friendship',
  PRIMARY KEY (`friendship_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4;

-- Volcando datos para la tabla social_network.network: ~4 rows (aproximadamente)
INSERT IGNORE INTO `network` (`friendship_id`, `user1_id`, `user2_id`, `relation_type`) VALUES
	(5, 1, 3, 'friendship'),
	(6, 4, 3, 'follow'),
	(8, 1, 5, 'follow'),
	(9, 5, 4, 'friendship');

-- Volcando estructura para tabla social_network.post
CREATE TABLE IF NOT EXISTS `post` (
  `post_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `text` varchar(50) NOT NULL,
  `visibility` varchar(50) NOT NULL DEFAULT 'private',
  `postedOn` timestamp NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`post_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4;

-- Volcando datos para la tabla social_network.post: ~7 rows (aproximadamente)
INSERT IGNORE INTO `post` (`post_id`, `user_id`, `text`, `visibility`, `postedOn`) VALUES
	(4, 1, 'first post', 'public', '2023-12-02 03:45:54'),
	(5, 1, 'second post', 'private', '2023-12-02 03:45:54'),
	(6, 1, 'third post', 'public', '2023-12-02 03:47:25'),
	(7, 3, 'four post', 'public', '2023-12-02 03:47:57'),
	(8, 3, 'five post', 'private', '2023-12-02 03:50:56'),
	(9, 2, 'six post', 'public', '2023-12-02 03:51:16'),
	(10, 5, 'Hello World', 'private', '2023-12-02 08:17:21');

-- Volcando estructura para tabla social_network.post_liked
CREATE TABLE IF NOT EXISTS `post_liked` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_like` int(11) NOT NULL,
  `post_liked` int(11) NOT NULL DEFAULT 0,
  `like_status` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4;

-- Volcando datos para la tabla social_network.post_liked: ~8 rows (aproximadamente)
INSERT IGNORE INTO `post_liked` (`id`, `user_like`, `post_liked`, `like_status`) VALUES
	(1, 1, 7, 1),
	(3, 1, 8, 1),
	(6, 1, 4, 1),
	(7, 1, 5, 1),
	(8, 1, 6, 1),
	(9, 3, 4, 1),
	(10, 3, 5, 1),
	(11, 3, 6, 1),
	(12, 1, 10, 0);

-- Volcando estructura para tabla social_network.user
CREATE TABLE IF NOT EXISTS `user` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `fullName` varchar(50) NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

-- Volcando datos para la tabla social_network.user: ~5 rows (aproximadamente)
INSERT IGNORE INTO `user` (`user_id`, `fullName`) VALUES
	(1, 'Hansel Tellez'),
	(2, 'Wesly Hill'),
	(3, 'Pedro Acosta'),
	(4, 'Jhon Doe'),
	(5, 'Rossana Rodriguez');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
