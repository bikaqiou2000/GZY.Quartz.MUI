/*
 Navicat Premium Data Transfer

 Source Server         : localmysql
 Source Server Type    : MySQL
 Source Server Version : 50527
 Source Host           : localhost:3306
 Source Schema         : quartz

 Target Server Type    : MySQL
 Target Server Version : 50527
 File Encoding         : 65001

 Date: 23/06/2025 13:33:15
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for tab_api_log
-- ----------------------------
DROP TABLE IF EXISTS `tab_api_log`;
CREATE TABLE `tab_api_log`  (
  `Id` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Logged` datetime NULL DEFAULT NULL,
  `Level` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Message` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `Logger` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Properties` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `Callsite` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `Exception` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for tab_quarz_task
-- ----------------------------
DROP TABLE IF EXISTS `tab_quarz_task`;
CREATE TABLE `tab_quarz_task`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `TaskName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '任务名',
  `GroupName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '分组名',
  `Interval` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '间隔时间',
  `ApiUrl` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '调用的API地址',
  `Describe` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '任务描述',
  `LastRunTime` datetime NULL DEFAULT NULL COMMENT '最近一次运行时间',
  `Status` int(11) NOT NULL COMMENT '运行状态',
  `TaskType` int(11) NOT NULL COMMENT '任务类型(1.DLL类型,2.API类型)',
  `ApiRequestType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT 'API访问类型(API类型)',
  `ApiAuthKey` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '授权名(API类型)',
  `ApiAuthValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '授权值(API类型)',
  `ApiParameter` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT 'API参数',
  `ApiTimeOut` int(11) NULL DEFAULT NULL COMMENT 'API超时时间',
  `DllClassName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT 'DLL类型名',
  `DllActionName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT 'Dll方法名',
  `timeflag` datetime NULL DEFAULT NULL,
  `changetime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '任务表' ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for tab_quarz_tasklog
-- ----------------------------
DROP TABLE IF EXISTS `tab_quarz_tasklog`;
CREATE TABLE `tab_quarz_tasklog`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `TaskName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '任务名',
  `GroupName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '分组名',
  `BeginDate` datetime NULL DEFAULT NULL COMMENT '任务开始时间',
  `EndDate` datetime NULL DEFAULT NULL COMMENT '任务结束时间',
  `Msg` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '任务执行结果',
  `State` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '状态',
  `timeflag` datetime NULL DEFAULT NULL,
  `changetime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '任务执行记录' ROW_FORMAT = Compact;

SET FOREIGN_KEY_CHECKS = 1;
