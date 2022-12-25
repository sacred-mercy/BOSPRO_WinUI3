-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 25, 2022 at 08:24 AM
-- Server version: 10.4.25-MariaDB
-- PHP Version: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bospro`
--

-- --------------------------------------------------------

--
-- Table structure for table `course`
--

CREATE TABLE `course` (
  `course_code` varchar(255) NOT NULL,
  `course_name` varchar(255) NOT NULL,
  `course_credit` varchar(255) NOT NULL,
  `course_ltp` varchar(255) NOT NULL,
  `course_semester` int(15) NOT NULL,
  `course_program_name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `course`
--

INSERT INTO `course` (`course_code`, `course_name`, `course_credit`, `course_ltp`, `course_semester`, `course_program_name`) VALUES
('BSIT-C-501', '.Net Technologies and Framework', '3', '4-2-3', 5, 'Bachelors of Information Technology'),
('BSIT-C-502', 'Computer Graphics And Animations', '2', '3-4-2', 5, 'Bachelors of Information Technology');

-- --------------------------------------------------------

--
-- Table structure for table `course_data`
--

CREATE TABLE `course_data` (
  `code` varchar(20) NOT NULL,
  `year` year(4) NOT NULL,
  `objective` text NOT NULL,
  `syllabus` longtext NOT NULL,
  `outcome` text NOT NULL,
  `reference` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `course_data`
--

INSERT INTO `course_data` (`code`, `year`, `objective`, `syllabus`, `outcome`, `reference`) VALUES
('BSIT-C-502', 2020, 'jbbi', 'hhiuhih', 'hiuhi', 'hiu'),
('BSIT-C-501', 2021, 'oihih', 'oi', 'hoi', 'ho'),
('BSIT-C-502', 2021, 'bygg', 'igiugiu', 'giugiug', 'iugig'),
('BSIT-C-501', 2025, 'ihiuh', 'iuhiuh', 'hiuhih', 'ihiuh');

-- --------------------------------------------------------

--
-- Table structure for table `programs`
--

CREATE TABLE `programs` (
  `program_code` varchar(10) NOT NULL,
  `program_name` tinytext NOT NULL,
  `program_college` tinytext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `programs`
--

INSERT INTO `programs` (`program_code`, `program_name`, `program_college`) VALUES
('BSCIT', 'Bachelors of Information Technology', 'USCS'),
('BCA', 'bachelors of Computer application', 'UIT');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `email` varchar(30) NOT NULL,
  `password` varchar(255) NOT NULL,
  `name` char(100) NOT NULL,
  `role` text NOT NULL,
  `college` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`email`, `password`, `name`, `role`, `college`) VALUES
('g', 'g', 'Gaurav Singh', 'user', 'USCS'),
('a', 'a', 'Admin Singh', 'admin', 'UIT'),
('f', 'f', 'Faurav Singh', 'user', 'UIM');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
