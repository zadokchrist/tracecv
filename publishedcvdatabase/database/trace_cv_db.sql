-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 14, 2024 at 12:23 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `trace_cv_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `contacts`
--

CREATE TABLE `contacts` (
  `Id` int(11) NOT NULL,
  `PhoneNumber` longtext NOT NULL,
  `ExpertId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `contacts`
--

INSERT INTO `contacts` (`Id`, `PhoneNumber`, `ExpertId`) VALUES
(7, '0779226226', 7);

-- --------------------------------------------------------

--
-- Table structure for table `educations`
--

CREATE TABLE `educations` (
  `Id` int(11) NOT NULL,
  `Level` longtext NOT NULL,
  `ExpertId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `educations`
--

INSERT INTO `educations` (`Id`, `Level`, `ExpertId`) VALUES
(7, 'Bachelors', 7);

-- --------------------------------------------------------

--
-- Table structure for table `experts`
--

CREATE TABLE `experts` (
  `Id` int(11) NOT NULL,
  `Name` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `Gender` longtext NOT NULL,
  `DOB` longtext NOT NULL,
  `Speciality` longtext NOT NULL,
  `CurrentEmployer` longtext DEFAULT NULL,
  `WorkedWith2ML` tinyint(1) NOT NULL,
  `CvFilePath` longtext NOT NULL,
  `Category` longtext NOT NULL,
  `Nationality` longtext NOT NULL,
  `experience` longtext NOT NULL,
  `lastedit` longtext NOT NULL,
  `Sector` longtext NOT NULL,
  `EmploymentStatus` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `experts`
--

INSERT INTO `experts` (`Id`, `Name`, `Email`, `Gender`, `DOB`, `Speciality`, `CurrentEmployer`, `WorkedWith2ML`, `CvFilePath`, `Category`, `Nationality`, `experience`, `lastedit`, `Sector`, `EmploymentStatus`) VALUES
(7, 'Daniel Ngobi', 'ngobizadokchrist@gmail.com', 'Male', '2024-02-14', 'ICT', 'Tracecorp Solutions Ltd', 1, '/cvs/a1b4e1df-35c5-453f-9e90-e193ad70ca06-Archievements For the week 5th February - 9th February 2024.docx', 'FullTime Staff', 'Albania', '10', '2022', 'Technology', 'Employed');

-- --------------------------------------------------------

--
-- Table structure for table `languages`
--

CREATE TABLE `languages` (
  `Id` int(11) NOT NULL,
  `Type` longtext NOT NULL,
  `ExpertId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `languages`
--

INSERT INTO `languages` (`Id`, `Type`, `ExpertId`) VALUES
(10, 'English', 7),
(11, 'Lusoga', 7),
(12, 'Luganda', 7);

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20240129121657_InitialCreate', '7.0.2'),
('20240129122107_InitialCore', '7.0.2'),
('20240129122610_LanguageCore', '7.0.2'),
('20240129124033_RemovingExpertInContactCore', '7.0.2'),
('20240129130231_UpdatedModel', '7.0.2'),
('20240129131819_UpdatedModel2', '7.0.2'),
('20240131065003_AdditionOfCategoryColumnOnExpert', '7.0.2'),
('20240201074107_AdditionOfExtraColumnsOnExpert', '7.0.2'),
('20240201092257_AdditionOfExtraColumnsOnExpert2', '7.0.2'),
('20240206035031_AdditionOfSector', '7.0.2'),
('20240206115423_AdditionOfEmploymentStatus', '7.0.2'),
('20240212073211_UpdatingCurrentEmployerToAllowNullValues', '7.0.2'),
('20240212073922_UpdatingCurrentEmployerToAllowNullValues1', '7.0.2');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `contacts`
--
ALTER TABLE `contacts`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Contacts_ExpertId` (`ExpertId`);

--
-- Indexes for table `educations`
--
ALTER TABLE `educations`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Educations_ExpertId` (`ExpertId`);

--
-- Indexes for table `experts`
--
ALTER TABLE `experts`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `languages`
--
ALTER TABLE `languages`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Languages_ExpertId` (`ExpertId`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `contacts`
--
ALTER TABLE `contacts`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `educations`
--
ALTER TABLE `educations`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `experts`
--
ALTER TABLE `experts`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `languages`
--
ALTER TABLE `languages`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `contacts`
--
ALTER TABLE `contacts`
  ADD CONSTRAINT `FK_Contacts_Experts_ExpertId` FOREIGN KEY (`ExpertId`) REFERENCES `experts` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `educations`
--
ALTER TABLE `educations`
  ADD CONSTRAINT `FK_Educations_Experts_ExpertId` FOREIGN KEY (`ExpertId`) REFERENCES `experts` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `languages`
--
ALTER TABLE `languages`
  ADD CONSTRAINT `FK_Languages_Experts_ExpertId` FOREIGN KEY (`ExpertId`) REFERENCES `experts` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
