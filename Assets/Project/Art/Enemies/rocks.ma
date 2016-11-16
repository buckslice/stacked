//Maya ASCII 2017 scene
//Name: rocks.ma
//Last modified: Tue, Nov 15, 2016 03:58:53 PM
//Codeset: 1252
requires maya "2017";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2017";
fileInfo "version" "2017";
fileInfo "cutIdentifier" "201606150345-997974";
fileInfo "osv" "Microsoft Windows 8 , 64-bit  (Build 9200)\n";
fileInfo "license" "student";
createNode transform -s -n "persp";
	rename -uid "DA72B273-434E-7449-7C10-929F80183CD9";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0.053515476819585295 -0.21277261185368143 1.6222375234824498 ;
	setAttr ".r" -type "double3" 12.861647269839489 -716.19999999982042 0 ;
createNode camera -s -n "perspShape" -p "persp";
	rename -uid "892F0F57-4DEC-46A1-7136-87B2748DB208";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".coi" 1.434402224234661;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".hc" -type "string" "viewSet -p %camera";
	setAttr ".ai_translator" -type "string" "perspective";
createNode transform -s -n "top";
	rename -uid "20BD2DC0-49EF-462B-0EDB-A2B235B1906D";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 1000.1 0 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	rename -uid "AB6B757B-4D62-4DA4-CC93-DEA6FB3F8343";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "front";
	rename -uid "2BE48907-496C-3F9D-F1B3-C486594FC009";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 0 1000.1 ;
createNode camera -s -n "frontShape" -p "front";
	rename -uid "391C8D4F-464D-E187-EFDC-78AD91B5A7DD";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 3.0605996531653434;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "side";
	rename -uid "D7539745-433B-DC34-C6BA-B781841A2A7D";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 1000.1 0 0 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	rename -uid "1DC85CD9-4CF5-15DB-46A0-5A822DEA693B";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -n "angry";
	rename -uid "078FB769-4E2E-CC65-076E-3F8BB296656E";
createNode mesh -n "angryShape" -p "angry";
	rename -uid "9CDDCD9F-471A-5CB1-2E36-A4AEB8916082";
	setAttr -k off ".v";
	setAttr -av ".iog[0].og[0].gid";
	setAttr -av ".iog[0].og[1].gid";
	setAttr -av ".iog[0].og[2].gid";
	setAttr -av ".iog[0].og[3].gid";
	setAttr -av ".iog[0].og[4].gid";
	setAttr -av ".iog[0].og[5].gid";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.44303695118287578 0.50000002258457243 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "derp";
	rename -uid "B5BBB188-4087-B03B-CD99-F287E60168CA";
createNode mesh -n "derpShape" -p "derp";
	rename -uid "5A06FC3A-4145-FB94-E77E-BC94177055B2";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.82321083545684814 0.56949701905250549 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "reaper";
	rename -uid "0A46B648-4FD0-C082-EDDB-C98379EC3C91";
createNode mesh -n "reaperShape" -p "reaper";
	rename -uid "34864A97-47ED-CB8E-6585-879707611A68";
	setAttr -k off ".v";
	setAttr -av ".iog[0].og[1].gid";
	setAttr -av ".iog[0].og[7].gid";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.77777886390686035 0.43630982935428619 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr -s 2 ".clst";
	setAttr ".clst[0].clsn" -type "string" "SculptFreezeColorTemp";
	setAttr ".clst[1].clsn" -type "string" "SculptMaskColorTemp";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "dreamworks";
	rename -uid "8D158DCD-4F42-B97E-D720-C9937AD13372";
createNode mesh -n "dreamworksShape" -p "dreamworks";
	rename -uid "EF4BADD4-4996-3662-DAA0-ADB8EBE4C609";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.50000005960464478 0.5 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr -s 2 ".clst";
	setAttr ".clst[0].clsn" -type "string" "SculptFreezeColorTemp";
	setAttr ".clst[1].clsn" -type "string" "SculptMaskColorTemp";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode lightLinker -s -n "lightLinker1";
	rename -uid "7C63080F-474C-4CAE-EC57-9489BB4D0637";
	setAttr -s 2 ".lnk";
	setAttr -s 2 ".slnk";
createNode displayLayerManager -n "layerManager";
	rename -uid "557B8B6D-4583-EBDB-7A84-B5A85ECE1F37";
createNode displayLayer -n "defaultLayer";
	rename -uid "40941BE9-46DE-5400-3F47-4093B89896DE";
createNode renderLayerManager -n "renderLayerManager";
	rename -uid "AAECA0A3-4F12-04DF-C56A-9AB0146812B5";
createNode renderLayer -n "defaultRenderLayer";
	rename -uid "7C5E843C-46DC-7A8F-37B3-439C4E709E92";
	setAttr ".g" yes;
createNode shapeEditorManager -n "shapeEditorManager";
	rename -uid "A5955DD6-451B-8F7D-3EA6-D0971CE9CBCE";
createNode poseInterpolatorManager -n "poseInterpolatorManager";
	rename -uid "197ADB1B-4095-65C5-94A7-07ABBB1BF0B0";
createNode polyPlatonicSolid -n "polyPlatonicSolid1";
	rename -uid "8B11435B-43D1-5E1E-1B6B-91A2961224CF";
	setAttr ".l" 0.71369999647140503;
	setAttr ".cuv" 4;
createNode polySmoothFace -n "polySmoothFace1";
	rename -uid "FBC38B51-41D7-B61C-EAA6-1AB8E9F8ACA8";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".sdt" 2;
	setAttr ".suv" yes;
	setAttr ".ps" 0.10000000149011612;
	setAttr ".ro" 1;
	setAttr ".ma" yes;
	setAttr ".m08" yes;
createNode polySubdFace -n "polySubdFace1";
	rename -uid "49A8443B-456C-5785-7EE1-62B09011E06A";
	setAttr ".ics" -type "componentList" 1 "f[0:59]";
createNode polyReduce -n "polyReduce1";
	rename -uid "A7A1846B-49B6-7486-CE0B-38899E8C236B";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".ver" 1;
	setAttr ".p" 33;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polyReduce -n "polyReduce2";
	rename -uid "01FF0836-449E-6A40-DB07-AD97F79EB0E2";
	setAttr ".ics" -type "componentList" 1 "f[0:145]";
	setAttr ".ver" 1;
	setAttr ".p" 33;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polySmoothFace -n "pasted__polySmoothFace1";
	rename -uid "9CE6644F-4121-991B-4646-0EB8A631BD49";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".sdt" 2;
	setAttr ".suv" yes;
	setAttr ".ps" 0.10000000149011612;
	setAttr ".ro" 1;
	setAttr ".ma" yes;
	setAttr ".m08" yes;
createNode polySubdFace -n "pasted__polySubdFace1";
	rename -uid "7E4E6F90-48EB-5816-E632-8F99CC56733D";
	setAttr ".ics" -type "componentList" 1 "f[0:59]";
createNode polyPlatonicSolid -n "pasted__polyPlatonicSolid1";
	rename -uid "3D0BA73D-4232-35A2-422A-DA9D27608441";
	setAttr ".l" 0.71369999647140503;
	setAttr ".cuv" 4;
createNode polySubdFace -n "polySubdFace2";
	rename -uid "B81C159D-4C55-7F03-FE96-47907E6D34DE";
	setAttr ".ics" -type "componentList" 3 "f[21]" "f[53:56]" "f[76]";
createNode polyTweak -n "polyTweak1";
	rename -uid "422BC29F-46EC-7183-4DD6-E489E1444991";
	setAttr ".uopa" yes;
	setAttr -s 42 ".tk";
	setAttr ".tk[5]" -type "float3" -3.7252903e-009 7.4505806e-009 0 ;
	setAttr ".tk[8]" -type "float3" -0.0032337932 -0.0051869 0 ;
	setAttr ".tk[9]" -type "float3" -0.014472095 -0.023212038 0 ;
	setAttr ".tk[10]" -type "float3" 0.0048296149 0.0077461363 0 ;
	setAttr ".tk[12]" -type "float3" -0.011748746 -0.018844079 0 ;
	setAttr ".tk[13]" -type "float3" -0.005329547 -0.0085483119 0 ;
	setAttr ".tk[14]" -type "float3" -0.0064086886 -0.010278989 0 ;
	setAttr ".tk[16]" -type "float3" -5.5879354e-009 0 0 ;
	setAttr ".tk[18]" -type "float3" -0.0075341277 -0.012084191 0 ;
	setAttr ".tk[20]" -type "float3" -0.0043128459 -0.0069176103 0 ;
	setAttr ".tk[22]" -type "float3" 0.0021063383 0.0033781962 0 ;
	setAttr ".tk[23]" -type "float3" -0.0021082787 -0.003381684 0 ;
	setAttr ".tk[24]" -type "float3" -0.0024917154 -0.0039967145 0 ;
	setAttr ".tk[32]" -type "float3" -0.0071507357 -0.01146919 0 ;
	setAttr ".tk[33]" -type "float3" -0.015826071 -0.025383677 0 ;
	setAttr ".tk[35]" -type "float3" -0.0071506761 -0.01146925 0 ;
	setAttr ".tk[40]" -type "float3" -0.0043128459 -0.0069176103 0 ;
	setAttr ".tk[45]" -type "float3" -5.5879354e-009 3.7252903e-009 0 ;
	setAttr ".tk[46]" -type "float3" -0.003233823 -0.0051869298 0 ;
	setAttr ".tk[47]" -type "float3" -0.0024917154 -0.0039966549 0 ;
	setAttr ".tk[49]" -type "float3" -0.0038542179 -0.0061820294 0 ;
	setAttr ".tk[50]" -type "float3" 0.10080905 0.16168913 0 ;
	setAttr ".tk[51]" -type "float3" -0.0021082787 -0.003381684 0 ;
	setAttr ".tk[52]" -type "float3" -0.0038542477 -0.0061820294 0 ;
	setAttr ".tk[62]" -type "float3" -0.005788235 -0.0092838295 0 ;
	setAttr ".tk[63]" -type "float3" -0.014472095 -0.023212038 0 ;
	setAttr ".tk[64]" -type "float3" -0.0064085992 -0.010278989 0 ;
	setAttr ".tk[65]" -type "float3" -0.0075341277 -0.012084191 0 ;
	setAttr ".tk[66]" -type "float3" -0.11107203 -0.17815027 0 ;
	setAttr ".tk[67]" -type "float3" -0.12817891 -0.20558828 0 ;
	setAttr ".tk[68]" -type "float3" -0.11045156 -0.17715511 0 ;
	setAttr ".tk[69]" -type "float3" -0.005329547 -0.0085481927 0 ;
	setAttr ".tk[70]" -type "float3" -0.11045156 -0.17715511 0 ;
	setAttr ".tk[71]" -type "float3" -0.005788235 -0.0092838295 0 ;
	setAttr ".tk[73]" -type "float3" -7.4505806e-009 -1.8626451e-009 0 ;
	setAttr ".tk[74]" -type "float3" 4.6566129e-010 -1.1175871e-008 0 ;
	setAttr ".tk[75]" -type "float3" 2.7939677e-009 5.5879354e-009 0 ;
	setAttr ".tk[76]" -type "float3" 4.8428774e-008 5.5879354e-009 0 ;
	setAttr ".tk[77]" -type "float3" 0 -1.3969839e-009 0 ;
createNode polyReduce -n "polyReduce3";
	rename -uid "B1DDA08F-4759-BD70-81DA-57ADB59F2515";
	setAttr ".ics" -type "componentList" 4 "f[21]" "f[53:56]" "f[76]" "f[88:105]";
	setAttr ".ver" 1;
	setAttr ".p" 33;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polySplit -n "polySplit1";
	rename -uid "E9E8102C-4780-646D-FF96-FE9B4DA76AE1";
	setAttr -s 3 ".e[0:2]"  0.5 0.5 0;
	setAttr -s 3 ".d[0:2]"  -2147483595 -2147483485 -2147483608;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit2";
	rename -uid "98A8E54C-4BC5-23A3-FB40-FDAF2B7352F2";
	setAttr -s 2 ".e[0:1]"  0 0;
	setAttr -s 2 ".d[0:1]"  -2147483454 -2147483610;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit3";
	rename -uid "55993C24-4C24-67C0-10E5-77BA2E8CCE65";
	setAttr -s 7 ".e[0:6]"  0.5 0.5 0.5 0.5 0.5 0.5 0;
	setAttr -s 7 ".d[0:6]"  -2147483460 -2147483459 -2147483461 -2147483635 -2147483537 -2147483611 
		-2147483546;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit4";
	rename -uid "BC3BE867-48ED-F5D3-1B7D-84B4E71AA7C6";
	setAttr -s 5 ".e[0:4]"  1 0.5 0.5 0.5 1;
	setAttr -s 5 ".d[0:4]"  -2147483534 -2147483452 -2147483484 -2147483492 -2147483596;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyExtrudeFace -n "polyExtrudeFace1";
	rename -uid "166D867A-4626-2B95-9C65-6AA23C96E53E";
	setAttr ".ics" -type "componentList" 4 "f[88]" "f[90]" "f[92]" "f[100]";
	setAttr ".ix" -type "matrix" -0.81447242258580377 0.52906747753934713 -0.23816439082567392 0
		 0.50780243874826869 0.84857975712961264 0.14848932281870789 0 0.28066235238899212 5.4539706084710831e-015 -0.95980656590350399 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.045515068 0.34905803 0.54355115 ;
	setAttr ".rs" 43086;
	setAttr ".lt" -type "double3" 3.9898639947466563e-017 -1.2490009027033011e-016 0.065144717096741814 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.39504888675997174 0.14131110690447274 0.43045382382869402 ;
	setAttr ".cbx" -type "double3" 0.47757141696693162 0.42623864835734743 0.71175643876592809 ;
createNode polyTweak -n "polyTweak2";
	rename -uid "96939BA3-4F28-445D-FDB8-BDB5FCCB9994";
	setAttr ".uopa" yes;
	setAttr -s 24 ".tk";
	setAttr ".tk[7]" -type "float3" 0.051503528 0.018491715 -3.7252903e-009 ;
	setAttr ".tk[29]" -type "float3" 0.0038698737 0.037915688 -3.7252903e-009 ;
	setAttr ".tk[57]" -type "float3" -0.018313749 -0.029373702 -0.011343118 ;
	setAttr ".tk[87]" -type "float3" 0.0038698737 0.037915688 -3.7252903e-009 ;
	setAttr ".tk[90]" -type "float3" -0.0012326498 -0.012175929 -0.0051188343 ;
	setAttr ".tk[92]" -type "float3" 0.0081654079 -0.0048295027 -0.0010236544 ;
	setAttr ".tk[93]" -type "float3" -0.018313747 -0.029373713 -0.011343119 ;
	setAttr ".tk[95]" -type "float3" -0.018313749 -0.029373709 -0.011343119 ;
	setAttr ".tk[96]" -type "float3" 0.051503528 0.018491715 -3.7252903e-009 ;
	setAttr ".tk[98]" -type "float3" -0.018313752 -0.029373705 -0.011343117 ;
	setAttr ".tk[103]" -type "float3" -0.024581654 -0.02150077 -0.0013877923 ;
	setAttr ".tk[104]" -type "float3" -0.017534213 -0.010317808 0.0051188269 ;
createNode script -n "uiConfigurationScriptNode";
	rename -uid "407D0483-42C6-286B-42FE-5F913BD41E77";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"top\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n"
		+ "                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n"
		+ "                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n"
		+ "                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 291\n                -height 200\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n"
		+ "                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n"
		+ "            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n"
		+ "            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 291\n            -height 200\n            -sceneRenderFilter 0\n            $editorName;\n"
		+ "        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"side\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n"
		+ "                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n"
		+ "                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n"
		+ "                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 290\n                -height 199\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n"
		+ "            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n"
		+ "            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n"
		+ "            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 290\n            -height 199\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"front\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n"
		+ "                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n"
		+ "                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n"
		+ "                -width 291\n                -height 199\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n"
		+ "            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n"
		+ "            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n"
		+ "            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 291\n            -height 199\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n"
		+ "                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n"
		+ "                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n"
		+ "                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 322\n                -height 393\n                -sceneRenderFilter 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n"
		+ "\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n"
		+ "            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n"
		+ "            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 322\n            -height 393\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n"
		+ "            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"ToggledOutliner\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\"ToggledOutliner\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            outlinerEditor -e \n                -showShapes 0\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 0\n                -showConnected 0\n                -showAnimCurvesOnly 0\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 1\n                -showAssets 1\n                -showContainedOnly 1\n                -showPublishedAsConnected 0\n"
		+ "                -showContainerContents 1\n                -ignoreDagHierarchy 0\n                -expandConnections 0\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 0\n                -highlightActive 1\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"defaultSetFilter\" \n                -showSetMembers 1\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -isSet 0\n                -isSetMember 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n"
		+ "                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 0\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                -renderFilterIndex 0\n                -selectionOrder \"chronological\" \n                -expandAttribute 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"ToggledOutliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 0\n            -showReferenceMembers 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n"
		+ "            -showMuteInfo 0\n            -organizeByLayer 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -isSet 0\n            -isSetMember 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n"
		+ "            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            -renderFilterIndex 0\n            -selectionOrder \"chronological\" \n            -expandAttribute 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            outlinerEditor -e \n"
		+ "                -showShapes 0\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 0\n                -showConnected 0\n                -showAnimCurvesOnly 0\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 1\n                -showAssets 1\n                -showContainedOnly 1\n                -showPublishedAsConnected 0\n                -showContainerContents 1\n                -ignoreDagHierarchy 0\n                -expandConnections 0\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 0\n                -highlightActive 1\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n"
		+ "                -transmitFilters 0\n                -setFilter \"defaultSetFilter\" \n                -showSetMembers 1\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 0\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 0\n            -showReferenceMembers 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n"
		+ "            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"graphEditor\" -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n"
		+ "                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n"
		+ "                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 1\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -showCurveNames 0\n                -showActiveCurveNames 0\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n"
		+ "                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                -valueLinesToggle 1\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n"
		+ "                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n"
		+ "                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 1\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -showCurveNames 0\n"
		+ "                -showActiveCurveNames 0\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                -valueLinesToggle 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dopeSheetPanel\" -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n"
		+ "                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n"
		+ "                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n"
		+ "                -displayValues 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n"
		+ "                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n"
		+ "                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n"
		+ "                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"timeEditorPanel\" (localizedPanelLabel(\"Time Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"timeEditorPanel\" -l (localizedPanelLabel(\"Time Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Time Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"clipEditorPanel\" -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels `;\n"
		+ "\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n"
		+ "                -initialized 0\n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"sequenceEditorPanel\" -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperGraphPanel\" -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n"
		+ "            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n"
		+ "                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n"
		+ "                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperShadePanel\" -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"visorPanel\" -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"nodeEditorPanel\" -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n"
		+ "                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -activeTab -1\n                -editorMode \"default\" \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n"
		+ "                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -activeTab -1\n                -editorMode \"default\" \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"createNodePanel\" -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"polyTexturePlacementPanel\" -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\tif ($useSceneConfig) {\n\t\tscriptedPanel -e -to $panelName;\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"renderWindowPanel\" -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"shapePanel\" (localizedPanelLabel(\"Shape Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\tshapePanel -unParent -l (localizedPanelLabel(\"Shape Editor\")) -mbv $menusOkayInPanels ;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tshapePanel -edit -l (localizedPanelLabel(\"Shape Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"posePanel\" (localizedPanelLabel(\"Pose Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\tposePanel -unParent -l (localizedPanelLabel(\"Pose Editor\")) -mbv $menusOkayInPanels ;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tposePanel -edit -l (localizedPanelLabel(\"Pose Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynRelEdPanel\" -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"relationshipPanel\" -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"referenceEditorPanel\" -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"componentEditorPanel\" -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynPaintScriptedPanelType\" -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"scriptEditorPanel\" -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"profilerPanel\" (localizedPanelLabel(\"Profiler Tool\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"profilerPanel\" -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"contentBrowserPanel\" (localizedPanelLabel(\"Content Browser\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"contentBrowserPanel\" -l (localizedPanelLabel(\"Content Browser\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Content Browser\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            outlinerEditor -e \n                -showShapes 0\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 0\n                -showConnected 0\n                -showAnimCurvesOnly 0\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 1\n                -showAssets 1\n                -showContainedOnly 1\n                -showPublishedAsConnected 0\n                -showContainerContents 1\n"
		+ "                -ignoreDagHierarchy 0\n                -expandConnections 0\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 0\n                -highlightActive 1\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"defaultSetFilter\" \n                -showSetMembers 1\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -isSet 0\n                -isSetMember 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n"
		+ "                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 0\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                -renderFilterIndex 0\n                -selectionOrder \"chronological\" \n                -expandAttribute 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 0\n            -showReferenceMembers 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n"
		+ "            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -isSet 0\n            -isSetMember 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n"
		+ "            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            -renderFilterIndex 0\n            -selectionOrder \"chronological\" \n            -expandAttribute 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-userCreated false\n\t\t\t\t-defaultImage \"\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n"
		+ "\t\t\t\t-removeAllPanels\n\t\t\t\t-ap true\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 322\\n    -height 393\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 322\\n    -height 393\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        setFocus `paneLayout -q -p1 $gMainPane`;\n        sceneUIReplacement -deleteRemaining;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 12 -divisions 5 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	rename -uid "9D7CEF28-45E7-420F-7855-2D9CC837CFF0";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 120 -ast 1 -aet 200 ";
	setAttr ".st" 6;
createNode deleteComponent -n "deleteComponent1";
	rename -uid "D49EB628-4A2E-D390-B1A2-4187056B194B";
	setAttr ".dc" -type "componentList" 4 "f[85]" "f[94]" "f[113]" "f[115]";
createNode polyAppend -n "polyAppend1";
	rename -uid "EED421FD-418D-64D5-C508-359FC6A6016A";
	setAttr -s 2 ".d[0:1]"  -2147483423 -2147483497;
	setAttr ".tx" 1;
createNode polyAppend -n "polyAppend2";
	rename -uid "AB0C58BC-46AA-2346-0B3B-B2B2A271D3B1";
	setAttr -s 3 ".d[0:2]"  -2147483459 -2147483424 -2147483407;
	setAttr ".tx" 1;
createNode polyAppend -n "polyAppend3";
	rename -uid "859C98F4-4EBE-376E-87CE-2E88C98562C5";
	setAttr -s 3 ".d[0:2]"  -2147483462 -2147483408 -2147483426;
	setAttr ".tx" 1;
createNode polyAppend -n "polyAppend4";
	rename -uid "F1A1DD3D-4128-0F89-77C4-018C979D1764";
	setAttr -s 2 ".d[0:1]"  -2147483419 -2147483586;
	setAttr ".tx" 1;
createNode polyAppend -n "polyAppend5";
	rename -uid "8B2EF267-4337-9C80-08D7-E5969F0E6C39";
	setAttr -s 3 ".d[0:2]"  -2147483405 -2147483627 -2147483421;
	setAttr ".tx" 1;
createNode polyAppend -n "polyAppend6";
	rename -uid "D028D5B0-41EC-D244-7622-B5B72E66026F";
	setAttr -s 3 ".d[0:2]"  -2147483406 -2147483420 -2147483480;
	setAttr ".tx" 1;
createNode polySplit -n "polySplit5";
	rename -uid "0182DA69-4F4B-9AC0-81CE-2184F335E584";
	setAttr -s 2 ".e[0:1]"  0 0;
	setAttr -s 2 ".d[0:1]"  -2147483488 -2147483439;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit6";
	rename -uid "28E84D2B-464A-0A17-EB5A-D891B5983E79";
	setAttr -s 2 ".e[0:1]"  0 1;
	setAttr -s 2 ".d[0:1]"  -2147483543 -2147483443;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit7";
	rename -uid "1D996BAD-4328-631D-8E12-D4B864B4B1E7";
	setAttr -s 2 ".e[0:1]"  0 1;
	setAttr -s 2 ".d[0:1]"  -2147483612 -2147483444;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit8";
	rename -uid "EB10016F-4582-48C4-571F-B780800CF13C";
	setAttr -s 2 ".e[0:1]"  0 1;
	setAttr -s 2 ".d[0:1]"  -2147483456 -2147483488;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyExtrudeFace -n "polyExtrudeFace2";
	rename -uid "98555237-45EF-22E4-3576-66BA8C41A327";
	setAttr ".ics" -type "componentList" 5 "f[17]" "f[50]" "f[105]" "f[124]" "f[126]";
	setAttr ".ix" -type "matrix" -0.81447242258580377 0.52906747753934713 -0.23816439082567392 0
		 0.50780243874826869 0.84857975712961264 0.14848932281870789 0 0.28066235238899212 5.4539706084710831e-015 -0.95980656590350399 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.023252362 -0.24661675 0.63531673 ;
	setAttr ".rs" 58471;
	setAttr ".lt" -type "double3" 5.2041704279304213e-018 5.377642775528102e-017 -0.043693655538800447 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.3554996631238112 -0.28558938293745384 0.56542338315132035 ;
	setAttr ".cbx" -type "double3" 0.38760120741972348 -0.074784208855937215 0.77520954580551127 ;
createNode polyTweak -n "polyTweak3";
	rename -uid "48E145C1-4BA0-B901-EA9F-E380EAE70866";
	setAttr ".uopa" yes;
	setAttr -s 26 ".tk";
	setAttr ".tk[56]" -type "float3" -4.6566129e-010 4.6566129e-010 3.7252903e-009 ;
	setAttr ".tk[57]" -type "float3" -4.6566129e-010 4.6566129e-010 3.7252903e-009 ;
	setAttr ".tk[104]" -type "float3" -4.6566129e-010 4.6566129e-010 3.7252903e-009 ;
	setAttr ".tk[105]" -type "float3" -4.6566129e-010 4.6566129e-010 3.7252903e-009 ;
	setAttr ".tk[117]" -type "float3" -1.8626451e-009 -9.3132257e-010 -1.8626451e-009 ;
	setAttr ".tk[118]" -type "float3" -1.8626451e-009 -9.3132257e-010 -1.8626451e-009 ;
	setAttr ".tk[123]" -type "float3" -0.00058052567 -0.027572572 0.0079409136 ;
	setAttr ".tk[124]" -type "float3" -0.019318132 -0.020305786 0.029656308 ;
	setAttr ".tk[125]" -type "float3" -0.0079530776 0.023860443 0.012177437 ;
	setAttr ".tk[126]" -type "float3" 0.027796442 0.0019792931 -0.0029426301 ;
	setAttr ".tk[127]" -type "float3" -0.033300377 0.014611688 0.0072941775 ;
	setAttr ".tk[128]" -type "float3" 0.043952588 0.0049468381 -0.038145203 ;
	setAttr ".tk[129]" -type "float3" 0.026963478 -0.020605095 -0.018419646 ;
	setAttr ".tk[130]" -type "float3" -0.026774921 0.048236974 -0.0082311956 ;
createNode polySplit -n "polySplit9";
	rename -uid "84FFDB2C-4A31-142D-0A42-86B7B46DC3D1";
	setAttr -s 6 ".e[0:5]"  1 0.5 0.5 0.5 0.5 1;
	setAttr -s 6 ".d[0:5]"  -2147483611 -2147483459 -2147483489 -2147483600 -2147483496 -2147483603;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyTweak -n "polyTweak4";
	rename -uid "F5FD55FA-4A45-A6F8-4E11-23883D20EA15";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk";
	setAttr ".tk[131]" -type "float3" -0.02628959 0.016390879 -0.0011635004 ;
createNode deleteComponent -n "deleteComponent2";
	rename -uid "4D16CFB0-492F-8334-00B3-1AB16D8368DE";
	setAttr ".dc" -type "componentList" 1 "e[268]";
createNode polyExtrudeFace -n "polyExtrudeFace3";
	rename -uid "07F1D1E7-490F-1290-582C-12893FAEFB61";
	setAttr ".ics" -type "componentList" 4 "f[8]" "f[16]" "f[78]" "f[99]";
	setAttr ".ix" -type "matrix" -0.81447242258580377 0.52906747753934713 -0.23816439082567392 0
		 0.50780243874826869 0.84857975712961264 0.14848932281870789 0 0.28066235238899212 5.4539706084710831e-015 -0.95980656590350399 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.027226677 0.21928245 0.58860362 ;
	setAttr ".rs" 35730;
	setAttr ".lt" -type "double3" 3.2959746043559335e-017 -1.0408340855860843e-017 -0.039149306337689253 ;
	setAttr ".ls" -type "double3" 0.78333332802793298 0.78333332802793298 0.78333332802793298 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.43797780575398809 -0.0016500633741988915 0.49598166385335829 ;
	setAttr ".cbx" -type "double3" 0.47757152242400036 0.32104594401610842 0.74154476775418443 ;
createNode polyTweak -n "polyTweak5";
	rename -uid "A73B929A-4155-156C-8EF8-9CB388B901FE";
	setAttr ".uopa" yes;
	setAttr -s 4 ".tk";
	setAttr ".tk[131]" -type "float3" -0.036932822 -0.023006333 0.0056051798 ;
createNode polySubdFace -n "pasted__pasted__polySubdFace1";
	rename -uid "1FFB3B75-4E46-7583-853A-73A1F52256AE";
	setAttr ".ics" -type "componentList" 1 "f[0:59]";
createNode polySmoothFace -n "pasted__pasted__polySmoothFace1";
	rename -uid "0E02D615-4E64-1BEA-8583-6AA2AC3A8B0F";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".sdt" 2;
	setAttr ".suv" yes;
	setAttr ".ps" 0.10000000149011612;
	setAttr ".ro" 1;
	setAttr ".ma" yes;
	setAttr ".m08" yes;
createNode polyPlatonicSolid -n "pasted__pasted__polyPlatonicSolid1";
	rename -uid "E970969D-4791-EC3D-4DB4-3D8B6C18DCC2";
	setAttr ".l" 0.71369999647140503;
	setAttr ".cuv" 4;
createNode polyReduce -n "polyReduce4";
	rename -uid "1E60C324-419C-31D0-F8A5-E492563257D7";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".ver" 1;
	setAttr ".p" 48.600000000000016;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polySmoothFace -n "polySmoothFace2";
	rename -uid "7B9FD741-45D3-DEC2-B832-E080AAB91C41";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".sdt" 2;
	setAttr ".ovb" 2;
	setAttr ".suv" yes;
	setAttr ".ps" 0.10000000149011612;
	setAttr ".ro" 1;
	setAttr ".ma" yes;
	setAttr ".m08" yes;
createNode polyReduce -n "polyReduce5";
	rename -uid "4331CBCB-43A6-ECF6-67E5-11825A5231D2";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".ver" 1;
	setAttr ".p" 40;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polyReduce -n "polyReduce6";
	rename -uid "14399A66-43D9-BC0A-4A88-81A88AEA1B12";
	setAttr ".ics" -type "componentList" 1 "f[0:289]";
	setAttr ".ver" 1;
	setAttr ".p" 48.600000000000016;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polySmoothFace -n "polySmoothFace3";
	rename -uid "B37F4B31-415A-ABA8-5DCE-549DA6757999";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".sdt" 2;
	setAttr ".ovb" 2;
	setAttr ".suv" yes;
	setAttr ".ps" 0.10000000149011612;
	setAttr ".ro" 1;
	setAttr ".ma" yes;
	setAttr ".m08" yes;
createNode polyReduce -n "polyReduce7";
	rename -uid "DEF2D42E-4F65-DD4D-6496-B5BDAB24765C";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".ver" 1;
	setAttr ".p" 40;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polyReduce -n "polyReduce8";
	rename -uid "07594EB8-4931-72D3-DA5C-5495573E757D";
	setAttr ".ics" -type "componentList" 1 "f[0:359]";
	setAttr ".ver" 1;
	setAttr ".p" 48.600000000000016;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polyTweak -n "polyTweak6";
	rename -uid "76622E86-45CC-E175-0457-1BACFABE14D7";
	setAttr ".uopa" yes;
	setAttr -s 38 ".tk";
	setAttr ".tk[0]" -type "float3" 0.014088469 -0.022304913 -0.025147095 ;
	setAttr ".tk[1]" -type "float3" 0.091459453 -0.0036323841 0.021247264 ;
	setAttr ".tk[2]" -type "float3" 0.064919978 -0.07260038 -0.021911005 ;
	setAttr ".tk[4]" -type "float3" 0.016280521 -0.039357707 -0.03958077 ;
	setAttr ".tk[9]" -type "float3" 0.027559165 -0.0074965488 0.0059369188 ;
	setAttr ".tk[15]" -type "float3" 0.069334313 -0.010248625 -0.022159634 ;
	setAttr ".tk[17]" -type "float3" 0.0026698834 0.027133115 0.016641682 ;
	setAttr ".tk[21]" -type "float3" 0.030747967 -0.031824023 0.042228755 ;
	setAttr ".tk[33]" -type "float3" 0.043991018 -0.0063801412 0.019100523 ;
	setAttr ".tk[34]" -type "float3" 0.061284669 0.11647536 -0.084901571 ;
	setAttr ".tk[35]" -type "float3" 0.055902146 -0.0027187222 -0.021510243 ;
	setAttr ".tk[36]" -type "float3" 0.041490406 -0.062891923 -0.054164186 ;
	setAttr ".tk[38]" -type "float3" -0.0221544 -0.068315119 0.018545136 ;
	setAttr ".tk[39]" -type "float3" 0.027559165 -0.0083510643 0.023146212 ;
	setAttr ".tk[40]" -type "float3" -0.00023271749 -0.057003882 0.051639009 ;
	setAttr ".tk[51]" -type "float3" 0.035799377 -0.057116732 -0.064267732 ;
	setAttr ".tk[54]" -type "float3" 0.0026698834 0.027133115 0.016641682 ;
	setAttr ".tk[56]" -type "float3" 0.069400474 0.03270239 -0.0033469261 ;
	setAttr ".tk[57]" -type "float3" 0.032014988 0.0024069406 -0.052161422 ;
	setAttr ".tk[58]" -type "float3" 0.07572943 0.022395911 0.006109023 ;
	setAttr ".tk[59]" -type "float3" 0.055902146 -0.0027187222 -0.021510243 ;
	setAttr ".tk[60]" -type "float3" 0.070870638 -0.055328306 -0.006472006 ;
	setAttr ".tk[64]" -type "float3" 0.0017189444 -0.042994723 -0.014872495 ;
	setAttr ".tk[68]" -type "float3" 0.027559165 -0.0072168987 0.015737092 ;
	setAttr ".tk[69]" -type "float3" 0.031331729 -0.025823055 -0.0023085831 ;
	setAttr ".tk[70]" -type "float3" 0.034473538 -0.028314391 -0.035117406 ;
	setAttr ".tk[71]" -type "float3" 0.047666039 -0.022705508 0.039355107 ;
	setAttr ".tk[84]" -type "float3" 0.048468046 0.079664066 0 ;
	setAttr ".tk[100]" -type "float3" 0.084549472 0.12932208 -0.051033575 ;
	setAttr ".tk[101]" -type "float3" 0.12210748 -0.021463893 0.057940263 ;
	setAttr ".tk[102]" -type "float3" 0.082620166 -0.024422042 0.012628123 ;
	setAttr ".tk[103]" -type "float3" 0.089579895 -0.028125238 0.017015789 ;
	setAttr ".tk[104]" -type "float3" 0.058958225 -0.072059989 -0.028758721 ;
	setAttr ".tk[109]" -type "float3" 0.029450966 -0.032424908 -0.063333549 ;
	setAttr ".tk[113]" -type "float3" 0.070305817 -0.013006067 0.035182685 ;
	setAttr ".tk[114]" -type "float3" 0.067190222 -0.0093782553 0.01080898 ;
	setAttr ".tk[123]" -type "float3" 0.048468046 0.079664066 0 ;
	setAttr ".tk[144]" -type "float3" 0.089579895 -0.028125238 0.017015789 ;
createNode polySplit -n "polySplit10";
	rename -uid "BCCF1B19-4758-8162-2599-52862E18179A";
	setAttr -s 17 ".e[0:16]"  0.40000001 0.40000001 0.40000001 0.40000001
		 0.40000001 0.60000002 0.40000001 0.40000001 0.60000002 0.40000001 0.40000001 0.60000002
		 0.40000001 0.60000002 0.40000001 0.40000001 0.40000001;
	setAttr -s 17 ".d[0:16]"  -2147483615 -2147483494 -2147483604 -2147483388 -2147483457 -2147483547 
		-2147483463 -2147483467 -2147483552 -2147483474 -2147483398 -2147483556 -2147483479 -2147483570 -2147483487 -2147483499 -2147483615;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit11";
	rename -uid "9070C1F2-4807-291F-E4F4-64917AB882DA";
	setAttr -s 19 ".e[0:18]"  0.5 0.5 0.5 0.5 0.5 0.5 0.5 0.5 0.5 0.5 0.5
		 0.5 0.5 0.5 0.5 0.5 0.5 0.5 0.5;
	setAttr -s 19 ".d[0:18]"  -2147483550 -2147483395 -2147483549 -2147483384 -2147483385 -2147483544 
		-2147483515 -2147483543 -2147483378 -2147483538 -2147483375 -2147483537 -2147483513 -2147483530 -2147483359 -2147483526 -2147483354 -2147483356 
		-2147483550;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyExtrudeFace -n "polyExtrudeFace4";
	rename -uid "5D919159-40BE-5317-8467-39809EFC71CD";
	setAttr ".ics" -type "componentList" 3 "f[105:106]" "f[160]" "f[170]";
	setAttr ".ix" -type "matrix" 0.7951143706303373 0.51976605289055455 0.31246821899146127 0
		 -0.48375193608975064 0.85430863876152696 -0.19010737509850728 0 -0.36575565880316918 -5.134781488891349e-016 0.93071091003246531 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.052583069 0.27948156 -0.48163122 ;
	setAttr ".rs" 50912;
	setAttr ".lt" -type "double3" -8.6736173798840355e-017 1.4918621893400541e-016 0.060267158087112888 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.32749001526463206 0.22910468577123766 -0.6118418850921935 ;
	setAttr ".cbx" -type "double3" 0.4125299840869972 0.42244709088888099 -0.39188430959088028 ;
createNode polyTweak -n "polyTweak7";
	rename -uid "7A1D1FB2-4968-37F9-2CCF-F38CE1FF3533";
	setAttr ".uopa" yes;
	setAttr -s 190 ".tk";
	setAttr ".tk[0:165]" -type "float3"  0.0048646615 0.016482692 -0.0024509064
		 -0.040495276 -0.032244984 0.04492642 -0.057947829 -0.049094737 0.037577298 0.04426562
		 -0.03335486 -0.075384252 0.022609128 0.036581635 -0.001356623 0.060773663 0.099890031
		 0 -0.019683143 -0.007524929 0.033275899 -0.014502412 -0.10007976 -0.012253417 0.063297011
		 0.10623497 0.00044884809 0.059942383 0.098523654 1.4551915e-011 0.014327657 -0.13057195
		 0.015072428 -0.04576838 -0.14284854 0.028515723 0.025607705 -0.10948305 -0.077094905
		 0.069736488 -0.084425464 -0.058789212 0.043991823 -0.0038845325 -0.030411921 0.040704858
		 0.066904239 0 0.02531063 0.035920143 -0.0023028567 0.039481055 0.064892687 -1.1641532e-010
		 0.055835031 -0.014175846 -0.020404231 0.034297783 -0.018133888 -0.070889615 0.016131394
		 0.024134329 -0.012667866 0.025181403 0.041754805 7.4683448e-005 0.089210719 0.14663038
		 0 0.069811597 0.11474507 1.4551915e-011 0.060337979 0.099173948 0 -0.019711088 0.0062692575
		 0.022920916 0.0033646547 0.037764702 0.0081223221 -0.058123477 -0.17536695 -0.023052409
		 0.034068558 -0.15074925 -0.036087934 0.052452654 -0.025536545 -0.051439509 0.033266626
		 0.02514513 -0.0083990237 0.054654889 -0.073362961 -0.02453275 0.042665705 -0.061350524
		 -0.0087016402 0.0057549872 0.020748204 0.002672069 -0.017188204 -0.005948747 0.00035803931
		 -0.062485922 0.0054965373 0.021642618 -0.069498755 -0.053688902 0.014123126 -0.014543626
		 -0.13588737 -0.064589821 0.013029842 0.022697333 -0.0053493832 0.053375766 0.087730557
		 -5.8207661e-011 0.014023165 0.028872835 0.0013305652 0.027862601 0.027697742 -0.0077359625
		 0.031487782 0.029730842 -0.011922199 0.074247226 0.12203577 -5.8207661e-011 -0.036496885
		 -0.02720337 0.046004292 -0.064066805 -0.09564168 0.035409573 0.022461094 -0.15282373
		 -0.020452894 0.061711911 -0.020988075 -0.025941024 0.048127662 -0.020743741 -0.015220688
		 0.028869398 -0.065946832 0.0024242881 0.073143341 0.12022136 -1.4551915e-011 -0.063346446
		 -0.041114595 0.0063539464 0.051309362 0.089135893 0.00098081212 0.065672033 0.11022746
		 0.00046699712 0.039085846 0.064243123 0 0.060167689 0.09889403 0 -0.028126689 -0.0069741928
		 0.023626573 -0.054906555 0.0093681961 0.012255247 -0.032248564 -0.014823312 0.031271208
		 -0.048277576 -0.057413623 0.011154277 -0.11184364 -0.10759731 0.02815304 0.042773269
		 -0.057560809 -0.082248092 0.017081939 -0.044317789 -0.076624274 0.0096271448 -0.033462286
		 -0.065244414 0.022081567 0.035511233 -0.002762584 0.018968346 0.029953988 -0.002764646
		 0.029186722 0.0020840599 -0.048960119 -0.020888908 -0.02980487 -0.016155902 0.062091358
		 0.10205589 0 0.026431816 0.043444403 0 0.026339397 0.0432925 -1.1641532e-010 0.028512688
		 0.04686461 0 0.045217186 -0.0053203758 -0.042430773 0.075352408 0.12385223 -1.1641532e-010
		 0.061938092 0.10180387 0 0.027482647 0.035301402 -0.0077090953 0.026722496 0.042549994
		 -0.00067778624 0.027029524 0.044426821 0 0.025609259 0.042092413 0 0.086219691 0.14171414
		 1.4551915e-011 0.060491789 0.099426769 0 0.016639713 0.046240266 0.0038585938 -0.026961084
		 -0.0026681933 0.029711578 -0.0057289414 0.028283486 0.013603997 -0.054011624 -0.12979575
		 0.039953206 -0.055252191 -0.14903206 0.0065855538 -0.10668305 -0.091604106 0.049072944
		 -0.015580153 -0.18322228 -0.022092778 0.0079076281 -0.17266613 -0.049269535 0.067684859
		 -0.079578213 -0.071570687 0.072998397 -0.054045737 -0.050899491 0.028204411 0.046357915
		 0 0.052759372 -0.0020275544 -0.02003647 0.063225299 -0.036842126 -0.026349962 0.019472376
		 -0.014990385 0.0076208711 0.021879736 0.039999086 0.0010825115 0.031202771 0.0073341629
		 -0.0044077057 0.027480369 0.037530821 -0.0017907262 -0.0078587458 0.024122287 0.014167035
		 0.0010355404 0.036024943 0.0087797847 -0.016081953 0.0030864689 0.012340412 -0.037564371
		 -0.023946486 0.037616663 -0.14344293 -0.011698247 0.029113863 -0.062861107 -0.0041037062
		 0.032901436 -0.0660991 -0.058045506 0.032864816 0.014475791 -0.068196677 -0.075386994
		 0.0075131217 -0.063952625 -0.067207083 0.035648555 -0.079363301 -0.081087917 0.0036382203
		 0.0095884074 -0.010020785 -0.041298512 -0.051396992 0.00098313298 -0.072478466 -0.087774917
		 -0.0078311833 -0.027971959 -0.05247169 -0.017746732 -0.035839006 -0.14749542 -0.050409857
		 0.035537746 0.058411349 5.8207661e-011 0.039669495 0.065202452 -2.910383e-011 0.038696684
		 0.0057044383 -0.026942424 0.036250968 0.018839853 -0.022691065 0.058132097 0.09554825
		 0 0.075964704 0.12485868 7.2759576e-012 0.0035150563 0.022969611 0.0058928998 0.021373
		 0.042140938 0.0014521437 0.049269307 0.081170514 3.871656e-005 -0.10329421 0.010559853
		 0.056049105 0.010075095 -0.00087874674 0.04708216 -0.009209862 -0.14375833 0.019925619
		 -0.044425644 -0.11861038 0.02454971 -0.078766122 -0.11628544 0.027081171 -0.083733745
		 -0.11349215 0.0038003284 -0.065539755 -0.1745352 0.016471578 0.0092092687 -0.14427906
		 0.0082246196 0.042122152 -0.14752901 -0.045941729 -0.0025126031 -0.17858507 -0.038939394
		 0.06237372 -0.12665208 -0.060925439 0.044883501 -0.13501911 -0.065895632 0.058531277
		 -0.099017598 -0.060162649 0.070520498 -0.037631024 -0.043472439 0.062271308 -0.028077552
		 -0.041606508 0.045611635 0.0060016043 -0.013245861 0.049635321 -0.10148069 -0.039712615
		 0.04238968 -0.10213345 -0.02942203 0.030647008 -0.10338594 -0.01523786 0.00021517841
		 -0.075282723 0.061147891 0.057305779 -0.054497216 -0.0031296152 0.016268868 -0.029378032
		 0.0139042 -0.055349514 -0.063807987 0.030386193 -0.01645432 0.0052731526 0.026493184
		 -0.033857051 -0.018270222 0.041620735 -0.023533607 -0.17007974 0.0096806483 0.056626409
		 -0.094016992 -0.069996007 -0.017555632 -0.009614815 0.027632535 0.027586192 0.047849938
		 0.00051231729 0.066783793 -0.048009455 -0.055385243 -0.0096783955 -0.095159635 0.053068556
		 0.0093262307 -0.059788574 0.026596244 0.016478498 0.046410903 0.0039475719 0.027874935
		 0.047519561 0.00034789275 -0.0022159112 -0.031332381 -0.038711131 -0.010542258 -0.056303833
		 -0.038461901 -0.058577288 -0.11163563 -0.024603685 -0.075038895 -0.13839445 -0.0064517776
		 -0.075148806 -0.13975778 0.023363868 -0.05829547 -0.11468387 0.03313918 -0.11642151
		 -0.051517837 0.077400647 -0.096308783 0.095314577 0.05429586 -0.030275844 -0.020053191
		 0.041979332 -0.02748899 -0.009467544 0.036859296;
	setAttr ".tk[166:189]" -0.020093571 0.0084919399 0.024961714 -0.015863989 0.016533729
		 0.016901823 0.011249958 0.031384397 0.0036727719 0.019074932 0.033765849 0.00050779403
		 0.017660271 0.027975604 -0.004985414 0.011183764 0.016369633 -0.012945357 0.090109184
		 -0.017969865 0.0040101181 -0.013226916 -0.11331414 0.034311675 -0.028430378 -0.14340036
		 0.024516856 -0.045411121 -0.17239752 0.013351278 -0.037554767 -0.17935801 -0.022349546
		 -0.01918989 -0.16284564 -0.044630125 -0.0028179586 -0.15396303 -0.057033498 0.035709597
		 -0.1220446 -0.071608916 0.047539305 -0.086276971 -0.075928204 0.055901941 -0.068714112
		 -0.077547811 0.059228171 -0.036781382 -0.053940874 0.051503565 -0.014630776 -0.035461459
		 0.042973608 0.013526469 -0.014478911 0.035541806 0.024420988 -0.0072372057 0.02246397
		 0.026651615 -0.00034584932 0.0011235757 0.011958109 0.011907829 -0.002067083 -0.0010461893
		 0.018070722 -0.0070589148 -0.033217877 0.030192737;
createNode polyTweak -n "polyTweak8";
	rename -uid "829F7296-4D87-0677-50F4-03A7652E9AB9";
	setAttr ".uopa" yes;
	setAttr -s 17 ".tk";
	setAttr ".tk[60]" -type "float3" -0.0024170747 0.015082452 -0.025202662 ;
	setAttr ".tk[85]" -type "float3" 0.0010149378 0.0064303642 -0.0062984936 ;
	setAttr ".tk[144]" -type "float3" -0.056086745 -0.062452745 -0.082812212 ;
	setAttr ".tk[152]" -type "float3" -0.05567006 -0.041942652 -0.049520977 ;
	setAttr ".tk[190]" -type "float3" 0.02042836 -0.0058486862 -0.019209187 ;
	setAttr ".tk[191]" -type "float3" 0.014612824 -0.0030081365 -0.013580768 ;
	setAttr ".tk[192]" -type "float3" 0.012988012 -0.0090510668 -0.023582974 ;
	setAttr ".tk[193]" -type "float3" 0.0077977604 -0.0077631543 -0.018981349 ;
	setAttr ".tk[194]" -type "float3" 0.0054680523 0.00085980026 -0.0073499484 ;
	setAttr ".tk[195]" -type "float3" -0.001779101 -0.0063480199 -0.015585165 ;
	setAttr ".tk[196]" -type "float3" -0.021196278 0.020326365 -0.0029799147 ;
	setAttr ".tk[197]" -type "float3" -0.027305949 0.016192317 -0.0094225509 ;
	setAttr ".tk[198]" -type "float3" -0.015441368 0.015484152 -0.0026922005 ;
	setAttr ".tk[199]" -type "float3" -0.020062897 0.0096055456 -0.010070456 ;
	setAttr ".tk[200]" -type "float3" -0.0060728705 0.006051708 -0.0031530568 ;
	setAttr ".tk[201]" -type "float3" -0.013510938 0.00098769437 -0.012949544 ;
createNode deleteComponent -n "deleteComponent3";
	rename -uid "DE3A4EB5-4E13-8BDA-D229-B5A07170DA62";
	setAttr ".dc" -type "componentList" 4 "f[61]" "f[135]" "f[189]" "f[194]";
createNode polyAppend -n "polyAppend7";
	rename -uid "ABDACC7A-48D1-159F-847C-61A7151310A6";
	setAttr -s 2 ".d[0:1]"  -2147483261 -2147483436;
	setAttr ".tx" 1;
createNode polyAppend -n "polyAppend8";
	rename -uid "26E6DE9A-4409-E349-1D1F-A895EC87A45B";
	setAttr -s 3 ".d[0:2]"  -2147483249 -2147483508 -2147483263;
	setAttr ".tx" 1;
createNode polyAppend -n "polyAppend9";
	rename -uid "4AEEB908-46CE-2901-6C3D-CAB9E33CC522";
	setAttr -s 3 ".d[0:2]"  -2147483250 -2147483262 -2147483507;
	setAttr ".tx" 1;
createNode polyAppend -n "polyAppend10";
	rename -uid "38C9EB13-46A5-B18B-DFEF-46A4F5C147EF";
	setAttr -s 2 ".d[0:1]"  -2147483272 -2147483369;
	setAttr ".tx" 1;
createNode polyAppend -n "polyAppend11";
	rename -uid "9D20B08D-4966-4509-92F2-318E81C7628B";
	setAttr -s 3 ".d[0:2]"  -2147483248 -2147483276 -2147483591;
	setAttr ".tx" 1;
createNode polyAppend -n "polyAppend12";
	rename -uid "FBB82BA5-4E92-52A5-1E98-C885CEC34835";
	setAttr -s 3 ".d[0:2]"  -2147483450 -2147483273 -2147483247;
	setAttr ".tx" 1;
createNode polyExtrudeFace -n "polyExtrudeFace5";
	rename -uid "6A6F108A-4F91-E592-1F98-FC85F77EEE75";
	setAttr ".ics" -type "componentList" 4 "f[26]" "f[51]" "f[159]" "f[185]";
	setAttr ".ix" -type "matrix" 0.7951143706303373 0.51976605289055455 0.31246821899146127 0
		 -0.48375193608975064 0.85430863876152696 -0.19010737509850728 0 -0.36575565880316918 -5.134781488891349e-016 0.93071091003246531 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.054331835 0.1848906 -0.49417916 ;
	setAttr ".rs" 64894;
	setAttr ".lt" -type "double3" 4.8572257327350599e-017 6.2450045135165055e-017 -0.055810806753371425 ;
	setAttr ".ls" -type "double3" 0.72000000529100372 0.72000000529100372 0.70291467330249113 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.39116119451831688 0.037942355003541685 -0.61184182961750033 ;
	setAttr ".cbx" -type "double3" 0.48385308697159662 0.29185081041871691 -0.45383022642563176 ;
createNode polyTweak -n "polyTweak9";
	rename -uid "5584288F-403C-E6AD-3FF3-3C8612A08E2F";
	setAttr ".uopa" yes;
	setAttr -s 33 ".tk";
	setAttr ".tk[1]" -type "float3" -0.0082495352 0.055893004 0.014073663 ;
	setAttr ".tk[2]" -type "float3" -0.022138964 0.019506523 -0.027887082 ;
	setAttr ".tk[7]" -type "float3" 0.007092223 0.0094924336 0.00037309219 ;
	setAttr ".tk[10]" -type "float3" -0.011482492 0.0037230877 -0.018073386 ;
	setAttr ".tk[11]" -type "float3" -0.004515117 0.0027470216 -0.029768609 ;
	setAttr ".tk[45]" -type "float3" -0.016870698 0.0097195525 -0.049530298 ;
	setAttr ".tk[60]" -type "float3" -0.017162183 0.047154371 -0.011445358 ;
	setAttr ".tk[85]" -type "float3" 0.012932604 0.030334676 -0.012006826 ;
	setAttr ".tk[86]" -type "float3" 0.0066405134 0.032228354 -0.017637789 ;
	setAttr ".tk[102]" -type "float3" 0.011462474 -0.0069738328 0.002838688 ;
	setAttr ".tk[103]" -type "float3" -0.013768208 -0.038039025 -0.011362037 ;
	setAttr ".tk[124]" -type "float3" -0.017183533 0.0058523384 -0.045095559 ;
	setAttr ".tk[125]" -type "float3" 0.0085734632 0.020157129 -0.0080222152 ;
	setAttr ".tk[141]" -type "float3" 0.0044688694 -0.012322756 -0.0079693403 ;
	setAttr ".tk[144]" -type "float3" -0.0067861192 -0.017280038 -0.0013830778 ;
	setAttr ".tk[152]" -type "float3" -0.0097752083 -0.0066919639 -0.0029078706 ;
	setAttr ".tk[161]" -type "float3" -0.039839815 0.02229166 -0.030504705 ;
	setAttr ".tk[162]" -type "float3" -0.0013044961 0.011924714 -0.018607557 ;
	setAttr ".tk[173]" -type "float3" -0.0033907066 0.0043926784 -0.013180875 ;
	setAttr ".tk[174]" -type "float3" -0.017057743 0.0058017229 -0.044755168 ;
	setAttr ".tk[190]" -type "float3" -0.01899297 0.0087421667 -0.0069764992 ;
	setAttr ".tk[191]" -type "float3" -0.024518237 -0.006017331 -0.011907607 ;
	setAttr ".tk[192]" -type "float3" -0.014600501 0.0070475051 -0.022511948 ;
	setAttr ".tk[193]" -type "float3" -0.014513743 0.016049022 -0.021462632 ;
	setAttr ".tk[194]" -type "float3" -0.020112492 -0.0099051325 -0.018353578 ;
	setAttr ".tk[195]" -type "float3" -0.0078406688 0.020143732 -0.030053873 ;
	setAttr ".tk[196]" -type "float3" 0.025385419 -0.028034056 -0.011230769 ;
	setAttr ".tk[197]" -type "float3" -0.028221114 0.023205221 -0.044912145 ;
	setAttr ".tk[198]" -type "float3" -0.0018516108 -0.014819604 -0.019233888 ;
	setAttr ".tk[199]" -type "float3" -0.0067256084 0.0046564098 -0.026830925 ;
	setAttr ".tk[200]" -type "float3" -0.011679095 -0.010405747 -0.0238628 ;
	setAttr ".tk[201]" -type "float3" 0.0022701421 0.018677901 -0.0317849 ;
createNode polySubdFace -n "pasted__pasted__pasted__polySubdFace1";
	rename -uid "08E044B5-4903-EFAC-37EC-E8B7BDE35740";
	setAttr ".ics" -type "componentList" 1 "f[0:59]";
createNode polySmoothFace -n "pasted__pasted__pasted__polySmoothFace1";
	rename -uid "B3515CF9-4BF4-75E5-33B9-2C86920D0102";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".sdt" 2;
	setAttr ".suv" yes;
	setAttr ".ps" 0.10000000149011612;
	setAttr ".ro" 1;
	setAttr ".ma" yes;
	setAttr ".m08" yes;
createNode polyPlatonicSolid -n "pasted__pasted__pasted__polyPlatonicSolid1";
	rename -uid "6E2D56A4-49A4-16EF-A0C1-60A30AF8A078";
	setAttr ".l" 0.71369999647140503;
	setAttr ".cuv" 4;
createNode polyReduce -n "polyReduce9";
	rename -uid "65C9431E-4734-E647-4FFC-4E89553297E1";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".ver" 1;
	setAttr ".p" 83.4507;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polyTweak -n "polyTweak10";
	rename -uid "23CAE030-4AEC-EB14-0817-C2A238C62FEC";
	setAttr ".uopa" yes;
	setAttr -s 258 ".tk";
	setAttr ".tk[4]" -type "float3" -0.0072927889 -0.011232994 0 ;
	setAttr ".tk[6]" -type "float3" -0.017744975 -0.027332619 0 ;
	setAttr ".tk[7]" -type "float3" -0.017589735 -0.027093545 0 ;
	setAttr ".tk[8]" -type "float3" -0.013837212 -0.021313505 0 ;
	setAttr ".tk[11]" -type "float3" -0.020401517 -0.031424481 0 ;
	setAttr ".tk[14]" -type "float3" -0.0033849902 -0.0052138153 0 ;
	setAttr ".tk[15]" -type "float3" -0.00072842574 -0.0011219224 0 ;
	setAttr ".tk[16]" -type "float3" -0.0029648964 -0.0045667472 0 ;
	setAttr ".tk[22]" -type "float3" -0.0029648666 -0.0045666876 0 ;
	setAttr ".tk[23]" -type "float3" -0.013586243 -0.020926801 0 ;
	setAttr ".tk[24]" -type "float3" -0.013586243 -0.020926801 0 ;
	setAttr ".tk[25]" -type "float3" -0.0035402304 -0.0054529193 0 ;
	setAttr ".tk[30]" -type "float3" -0.0075436719 -0.011619585 0 ;
	setAttr ".tk[31]" -type "float3" -0.018165084 -0.027979687 0 ;
	setAttr ".tk[249]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[250]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[252]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[259]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[265]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[266]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[268]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[279]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[287]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[296]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[298]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[301]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[302]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[304]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[306]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[310]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[312]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[314]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[315]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[331]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[355]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[358]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[360]" -type "float3" 2.3841858e-007 5.9604645e-008 0 ;
	setAttr ".tk[364]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[366]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[367]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[368]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[370]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[371]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[380]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[387]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[389]" -type "float3" 0 1.4901161e-008 0 ;
	setAttr ".tk[392]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[393]" -type "float3" 0 1.4901161e-008 0 ;
	setAttr ".tk[394]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[396]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[398]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[399]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[403]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[405]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[413]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[421]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[422]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[429]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[437]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[438]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[441]" -type "float3" 0 1.4901161e-008 0 ;
	setAttr ".tk[447]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[449]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[460]" -type "float3" 0 1.4901161e-008 0 ;
	setAttr ".tk[462]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[470]" -type "float3" 2.3841858e-007 2.9802322e-008 0 ;
	setAttr ".tk[472]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[475]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[477]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[478]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[479]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[483]" -type "float3" 2.3841858e-007 0 0 ;
createNode deleteComponent -n "deleteComponent4";
	rename -uid "C1EA4C2F-4B1C-9356-A1F0-EE8B514A928A";
	setAttr ".dc" -type "componentList" 1 "e[23]";
createNode polySplit -n "polySplit12";
	rename -uid "4CEEF36D-4212-051D-8A19-4D86291EA40F";
	setAttr -s 2 ".e[0:1]"  0 1;
	setAttr -s 2 ".d[0:1]"  -2147483606 -2147483588;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySubdFace -n "polySubdFace3";
	rename -uid "19C6D56F-4CC5-EEF4-AFF0-CCA513F947B7";
	setAttr ".ics" -type "componentList" 3 "f[0:10]" "f[14:19]" "f[22:33]";
createNode polyReduce -n "polyReduce10";
	rename -uid "8FACB06A-4048-5862-C35D-4B9EE12B026F";
	setAttr ".ics" -type "componentList" 3 "f[0:10]" "f[14:19]" "f[22:118]";
	setAttr ".ver" 1;
	setAttr ".p" 21.1268;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polySmoothFace -n "polySmoothFace4";
	rename -uid "21AB1062-4ED1-4913-63E8-0C8E892179BB";
	setAttr ".ics" -type "componentList" 1 "f[14:15]";
	setAttr ".sdt" 2;
	setAttr ".ovb" 2;
	setAttr ".suv" yes;
	setAttr ".ps" 0.10000000149011612;
	setAttr ".ro" 1;
	setAttr ".ma" yes;
	setAttr ".m08" yes;
createNode createColorSet -n "createColorSet1";
	rename -uid "2FD74DAB-4750-6D03-05DF-1F8BC34EBC06";
	setAttr ".colos" -type "string" "SculptFreezeColorTemp";
	setAttr ".clam" no;
createNode createColorSet -n "createColorSet2";
	rename -uid "FE28D7C9-4519-DE25-52F9-9E97240937B9";
	setAttr ".colos" -type "string" "SculptMaskColorTemp";
	setAttr ".clam" no;
createNode polyTweak -n "polyTweak11";
	rename -uid "83C63128-49C6-0899-BF84-D79F4BCE2FC8";
	setAttr ".uopa" yes;
	setAttr -s 106 ".tk[0:105]" -type "float3"  -0.0090785176 -0.027946874
		 -0.01228261 0 0 0 0 0 0 7.6800585e-005 -0.00076293945 3.4272671e-005 0 0 0 -0.0013142824
		 -0.037135243 0.15378714 0.0049977899 0.015890688 -0.09606263 0.0011104196 -0.010396719
		 -0.0045152605 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 -0.0039815009 -0.023136586 -0.038988411
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 -0.00050112605 -8.1211329e-007 0.00081425905 -0.0028375089
		 0.00067350379 0.0058068633 0.014048335 -0.0032614297 -0.0058359033 0.0092826784 -0.020491719
		 0.018079937 -0.0084837675 -0.0063842237 0.013871104 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
		 0 0 0 -0.013397038 -8.5152686e-005 0.01818049 0 0 0 0.00073122978 8.7678432e-005
		 -0.0012536943 -0.00034841895 -0.00036768615 -2.014637e-005 0.0019595325 -0.002806142
		 -0.0031452626 0.00085127354 -0.017438754 -0.0041967183 0 0 0 0.00081691146 0.00043943524
		 -0.0076189935 0.0042434484 -0.00063878298 -0.058096051 0.00016537309 -0.0082836151
		 0.0035097301 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
		 0 0 0 0 0 0 0 0 -0.14871056 0.078522786 0 0 0 0 0 0 0 0 0 0 0 0 0 0.043864802 -0.059976906
		 0 7.1923369e-006 1.0313406e-005 4.1723251e-007 -6.1824918e-005 0.00036922097 0.00038722157
		 0.00016380847 1.552701e-005 0.00026422739 -0.046714853 0.025341904 0 0.0023853183
		 -0.011695623 0.0041434914 -0.018857542 -0.02023679 0.0059511065 0.020325363 0.0013600141
		 0.0065276623 -0.048381001 -0.048421144 0.035167783 5.4885633e-005 0.00026136637 -0.0014069378
		 -0.00090908073 -0.00066143274 -0.00037831068 -0.0015663803 -0.00013327599 -0.00056919456
		 0.0026019067 -0.012104511 -0.0080989003 0 0 0 0 0 0 0.00055775046 -0.00073641539
		 -0.00045409799 0 0 0 0 0 0 0 0 0 0.0015090406 -0.00015342236 0.0049312413 0.00066044927
		 -0.0027419925 0.0055600852 0.00015488267 -0.0017948151 0.0010391772 0.016253293 0.0038363934
		 0.018340692 0.00020614266 0.00083196163 -0.036432564 0.00048965216 -0.00075340271
		 0.000742428 -0.0093548894 -0.009077847 -0.036405861 0 0 0 0 0 0 -0.046714853 0.025341904
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0.080152594 0.11868879 0.0028975755 0.088101201 0.13471738
		 0.00081910565 0.033610493 0.028577179 -0.15325353 -0.00036758184 0.00092934072 -0.00054980919
		 -0.0037127733 0.0076957792 0.0045594573 0.0082060993 0.036770135 0.087535441 0.070580781
		 -0.0073598325 0.13086623 0.060258925 0.083297521 -0.0043281503 0.0054482631 0.002669394
		 -0.017532557 -0.0042918026 0.025989592 -0.055998594 0.0062810946 0.01430434 0.12667504
		 0.018069908 0.0035920143 0.027079538 0.065546498 0.008826673 -0.010271497;
createNode polySplit -n "polySplit13";
	rename -uid "52061B20-4E39-2E23-A670-C1A86862C5CB";
	setAttr -s 2 ".e[0:1]"  0.5 0.5;
	setAttr -s 2 ".d[0:1]"  -2147483530 -2147483477;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
	setAttr ".ief" yes;
createNode polyTweak -n "polyTweak12";
	rename -uid "B28CD463-4B43-5AAF-5394-5C9D9076D1E2";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk";
	setAttr ".tk[61]" -type "float3" -0.064366177 -0.010361558 0.014285063 ;
	setAttr ".tk[62]" -type "float3" -0.023722431 -0.075698204 -0.014275376 ;
	setAttr ".tk[64]" -type "float3" 0.018174743 -0.060787287 -0.014285063 ;
	setAttr ".tk[67]" -type "float3" -0.079537526 -0.03837112 0.0064255609 ;
	setAttr ".tk[106]" -type "float3" 0.014359667 0.02211825 0 ;
	setAttr ".tk[107]" -type "float3" 0.014359667 0.02211825 0 ;
createNode deleteComponent -n "deleteComponent5";
	rename -uid "595EB5D1-45F8-5792-68D5-109FB4ECEE3E";
	setAttr ".dc" -type "componentList" 1 "e[15]";
createNode deleteComponent -n "deleteComponent6";
	rename -uid "672D3773-4CE5-9FCA-C049-9F8002DD5D2B";
	setAttr ".dc" -type "componentList" 1 "e[120]";
createNode polySplit -n "polySplit14";
	rename -uid "70AF4782-46E4-6357-C628-868CD2F2B80E";
	setAttr -s 2 ".e[0:1]"  0 1;
	setAttr -s 2 ".d[0:1]"  -2147483525 -2147483531;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit15";
	rename -uid "117FD4C8-4612-6A10-FCD2-7D84AFBBCA8D";
	setAttr -s 2 ".e[0:1]"  0 1;
	setAttr -s 2 ".d[0:1]"  -2147483529 -2147483531;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode deleteComponent -n "deleteComponent7";
	rename -uid "D7F2E4C6-4C08-5859-23AD-0899CA2364A7";
	setAttr ".dc" -type "componentList" 1 "e[203]";
createNode polyExtrudeFace -n "polyExtrudeFace6";
	rename -uid "C7865C63-41E7-FB95-F5E3-11975B6E66BA";
	setAttr ".ics" -type "componentList" 2 "f[58]" "f[60]";
	setAttr ".ix" -type "matrix" -0.80507248138930487 0.54452990804635171 -0.23525619854256533 0
		 0.52267126770053829 0.83874142573443433 0.15273364618603327 0 0.28048715769171911 -2.6229018956769323e-014 -0.9598577781994686 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.011101749 0.3924205 0.45160988 ;
	setAttr ".rs" 51568;
	setAttr ".lt" -type "double3" 3.4694469519536142e-017 -3.4694469519536142e-017 0.12030680234212705 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.46708905838165438 0.19313709832484494 0.31481109050533129 ;
	setAttr ".cbx" -type "double3" 0.5141670209241187 0.51880180165324552 0.71619988202273255 ;
createNode polyTweak -n "polyTweak13";
	rename -uid "517EE035-40F8-B3E8-EE29-E5BE1462CACD";
	setAttr ".uopa" yes;
	setAttr -s 108 ".tk[0:107]" -type "float3"  0.020261567 -0.013154265 0.073882401
		 0.003499093 -0.0022716897 0.045821704 -0.01906726 0.012378896 -0.078570269 -0.0055484343
		 0.0036021685 -0.051611077 -0.014531879 0.0094344243 -0.059531018 -0.0083253086 0.022566905
		 -0.067018352 0.013953485 -0.0090589169 0.04886594 0.013653529 -0.0088641783 0.023187529
		 0.020995734 -0.013630904 0.05360901 0.0014501992 -0.00094150158 0.040011443 -0.0061795353
		 0.0040118946 0.0082853902 0.015604471 -0.010130776 0.065076239 0.0072146575 -0.0046839202
		 0.0019448813 -0.012241919 0.0079477308 -0.059633814 0.018751472 -0.012173884 0.06752266
		 -0.0061001927 0.0039603813 0.014076225 0.010741808 -0.0069738254 0.019000318 -0.0016814311
		 0.001091624 0.019558042 -0.0063919383 0.0041497913 0.002555727 -0.014586412 0.0094698267
		 -0.027006177 -0.019503562 0.012662153 -0.018687729 -0.018971914 0.012316997 -0.050490532
		 -0.016994091 0.011032947 -0.07882005 -0.0025448948 0.0016522037 -0.033924825 -0.013841602
		 0.0089862822 -0.034683816 0.015669389 -0.010172924 0.026901281 -0.012720172 0.0082582217
		 -0.016920054 -0.0071386439 0.004634568 0.006836439 -0.010884791 0.0070666531 -0.0064125247
		 -0.0081770867 0.0053087519 -0.00040335345 -0.0076958737 0.0049963347 -0.0011741195
		 -0.016131904 0.010473195 -0.041969933 -0.007214657 0.0046839193 -0.0019448851 0.010660587
		 -0.0069210944 0.055596963 0.01198295 -0.007779602 0.060060177 0.0049585528 -0.003219204
		 0.03827123 0.0057289884 -0.0037193878 0.035415843 -0.0023249963 0.0015094399 0.027043836
		 0.005192975 -0.0033713968 0.031651121 0.016630745 -0.010797055 0.059269361 0.0048945928
		 -0.0031776798 0.029288046 -0.0024708698 0.0016041446 0.021283589 0.00018624106 -0.00012091134
		 0.029735858 -0.0059820744 0.003883699 0.0073689837 -0.0090785399 0.0058939941 -0.0086317742
		 -0.0080314716 0.0052142134 0.0027628338 0.018290829 -0.01187482 0.06186799 0.018480843
		 -0.011998183 0.07177186 0.021018121 -0.013645439 0.06632375 0.015343212 -0.0099611627
		 0.035380855 0.014936712 -0.0096972538 0.050566003 0.012979689 -0.0084267082 0.061517652
		 0.0093167732 -0.0060486598 0.050818298 -0.017986236 0.011677071 -0.047795158 -0.016879477
		 0.010958537 -0.047427107 -0.019957645 0.012956955 -0.068502381 -0.015722714 0.010207544
		 -0.044132516 -0.016034069 0.010409682 -0.065753736 -0.014293494 0.0092796572 -0.039623741
		 -0.017402275 0.011297952 -0.071527913 -0.019574285 0.012708066 -0.065590277 -0.007341546
		 0.040090993 -0.15175171 0.092426941 0.032311849 -0.17942891 -0.0074035381 0.022969304
		 -0.12381037 0.0070768045 0.030730268 -0.11187498 -0.0041899541 0.0027202135 -0.043348085
		 0.0049530417 -0.088594764 -0.1807252 -0.074089728 0.13992415 -0.14290965 -0.18817548
		 0.057970416 -0.10967938 0.015681993 -0.010181102 0.043967966 0.016869361 -0.010951971
		 0.047371749 0.014280233 -0.0092710499 0.039517701 0.012517243 -0.008126474 0.021066764
		 0.017986236 -0.01167707 0.047795158 0.020472704 -0.013291346 0.06251128 0.014723032
		 -0.0095585268 0.025152868 0.018332561 -0.011901909 0.040255141 0.0214804 -0.013945558
		 0.058123484 0.014105194 -0.0091574145 0.027776947 0.0083562518 -0.0054250672 -0.00051369035
		 0.0063400553 -0.0041161072 -0.0076100617 0.0071373689 -0.0046337401 -0.0064029428
		 0.006826784 -0.0044321041 -0.0039922222 0.0040652817 -0.0026392718 -0.014051417 -0.001430447
		 0.00092867832 -0.039901685 0.010681817 -0.0069348789 0.015773008 -0.0033427144 0.0021701658
		 -0.036975816 0.0028822301 -0.0018712083 -0.019033283 -0.0079896031 0.0051870323 -0.057722025
		 -0.01354839 0.0087959226 -0.065638036 -0.0025136305 0.0016319051 -0.028844465 -0.014854868
		 0.0096441153 -0.07164228 0.0091332188 -0.0059294915 0.0051112641 -0.0049876194 0.0032380733
		 -0.010505468 -0.0003172828 0.0002059871 -0.0054314807 0.0061759572 -0.0040095709
		 0.027940746 -0.0012195247 0.00079174346 0.013309721 -0.010582552 0.0068704304 -0.018767865
		 -0.011936183 0.0077492408 -0.034301609 -0.011538714 0.0074911937 -0.041388601 0.0023425659
		 -0.0015208465 -0.0058805579 0.012493722 -0.0081112068 0.027934359 0.013932183 -0.0090450877
		 0.042826865 -0.0086523583 0.0056173066 -0.044319715 -0.0054010749 0.0035064998 -0.040284701
		 0.0039055843 -0.002535593 -0.0076882453 -0.01828579 0.011871548 -0.079718642 -0.016387668
		 0.010639246 -0.07301075;
createNode polyExtrudeFace -n "polyExtrudeFace7";
	rename -uid "A0BDEF68-4B1D-034E-4BA4-99BEE468B476";
	setAttr ".ics" -type "componentList" 2 "f[13]" "f[57]";
	setAttr ".ix" -type "matrix" -0.80507248138930487 0.54452990804635171 -0.23525619854256533 0
		 0.52267126770053829 0.83874142573443433 0.15273364618603327 0 0.28048715769171911 -2.6229018956769323e-014 -0.9598577781994686 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.029081119 0.26556581 0.51952076 ;
	setAttr ".rs" 48019;
	setAttr ".lt" -type "double3" 5.2041704279304213e-017 -6.9388939039072284e-017 -0.082961926393896504 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.52715312011522619 -0.027448486826057848 0.40521162444891923 ;
	setAttr ".cbx" -type "double3" 0.5674451907297815 0.35984898627049816 0.72814404075806649 ;
createNode polyTweak -n "polyTweak14";
	rename -uid "0ABD6EF6-434D-DED9-96FD-AFADF4EE45C6";
	setAttr ".uopa" yes;
	setAttr -s 12 ".tk";
	setAttr ".tk[106]" -type "float3" 0.025493868 0.039268304 -1.5543122e-015 ;
	setAttr ".tk[107]" -type "float3" 0.025493868 0.039268304 -1.5543122e-015 ;
	setAttr ".tk[118]" -type "float3" -0.047694508 0.020867318 0.0095662149 ;
	setAttr ".tk[119]" -type "float3" -0.05086286 0.0098356782 -0.00036343941 ;
	setAttr ".tk[120]" -type "float3" 0.0033262956 0.019194091 0.018835049 ;
	setAttr ".tk[121]" -type "float3" 0.0023649 -0.0048646973 0.013999304 ;
	setAttr ".tk[122]" -type "float3" 0.036725558 -0.030931173 -0.022296611 ;
	setAttr ".tk[123]" -type "float3" 0.039272979 -0.0023112786 0.0023758081 ;
	setAttr ".tk[124]" -type "float3" 0.024046844 -0.037012853 -0.028379412 ;
	setAttr ".tk[125]" -type "float3" 0.0096771736 -0.0099677602 0.0091292141 ;
createNode deleteComponent -n "deleteComponent8";
	rename -uid "2B12257F-492D-DD60-889A-95B4F2CFEC20";
	setAttr ".dc" -type "componentList" 1 "e[161]";
createNode deleteComponent -n "deleteComponent9";
	rename -uid "A6F9057D-424D-4F4B-129E-5FB7BAC6E8D0";
	setAttr ".dc" -type "componentList" 1 "e[120]";
createNode polySplit -n "polySplit16";
	rename -uid "B20B9CB4-4A0B-9A6B-37E4-6C80AE0174E3";
	setAttr -s 2 ".e[0:1]"  0 0;
	setAttr -s 2 ".d[0:1]"  -2147483596 -2147483485;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit17";
	rename -uid "491A0E28-4262-3CC9-3B44-AA9AF76E71C1";
	setAttr -s 2 ".e[0:1]"  1 1;
	setAttr -s 2 ".d[0:1]"  -2147483484 -2147483596;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit18";
	rename -uid "8D3BB570-40E8-B0F0-A9AA-88B020C10F10";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483599 -2147483458;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyTweak -n "polyTweak15";
	rename -uid "BD1A284F-4C47-13E5-96CD-18A5F2CB752B";
	setAttr ".uopa" yes;
	setAttr -s 50 ".tk";
	setAttr ".tk[2]" -type "float3" 0.026621066 -0.013882287 -0.0087336488 ;
	setAttr ".tk[3]" -type "float3" -0.099528454 -0.13247639 -0.0036652889 ;
	setAttr ".tk[21]" -type "float3" -0.20662269 -0.063502118 -0.0078604054 ;
	setAttr ".tk[22]" -type "float3" -0.053851027 0.036560308 0.022004515 ;
	setAttr ".tk[31]" -type "float3" -9.3132257e-010 3.7252903e-009 5.5879354e-009 ;
	setAttr ".tk[55]" -type "float3" 0.051277258 -0.01645579 -0.01680569 ;
	setAttr ".tk[59]" -type "float3" 0.026621066 -0.013882287 -0.0087336488 ;
	setAttr ".tk[60]" -type "float3" 0.067819044 -0.036744546 -0.02717118 ;
	setAttr ".tk[61]" -type "float3" -0.015276224 0.16419297 0.074285515 ;
	setAttr ".tk[62]" -type "float3" 0 -6.519258e-009 1.3969839e-009 ;
	setAttr ".tk[64]" -type "float3" 0.16058545 0.044891153 -0.0337677 ;
	setAttr ".tk[65]" -type "float3" -5.5879354e-009 7.4505806e-009 1.9499566e-009 ;
	setAttr ".tk[67]" -type "float3" 1.1175871e-008 -7.4505806e-009 1.8626451e-009 ;
	setAttr ".tk[86]" -type "float3" -0.008390341 0.061643865 -0.0012981745 ;
	setAttr ".tk[88]" -type "float3" -0.039380535 0.087307528 0.028970022 ;
	setAttr ".tk[91]" -type "float3" -0.021576699 0.0098310225 0.0038642688 ;
	setAttr ".tk[106]" -type "float3" -0.0040956801 0.00020964519 0.0022934228 ;
	setAttr ".tk[107]" -type "float3" 0.0094685247 -0.01121144 -0.0053608995 ;
	setAttr ".tk[108]" -type "float3" 0.070262857 -0.041830305 -0.04354639 ;
	setAttr ".tk[109]" -type "float3" 0.047852412 -0.052514035 -0.055768739 ;
	setAttr ".tk[110]" -type "float3" 0.034006916 -0.048590664 -0.039967727 ;
	setAttr ".tk[111]" -type "float3" 0.025688633 -0.02054332 0.0037561136 ;
	setAttr ".tk[112]" -type "float3" 0.045803085 0.0042052343 0.020299898 ;
	setAttr ".tk[113]" -type "float3" -0.011817163 0.0053801965 0.015209591 ;
	setAttr ".tk[114]" -type "float3" -0.069818005 0.011385787 -0.010655056 ;
	setAttr ".tk[115]" -type "float3" -0.085065849 0.030465741 -0.0022059819 ;
	setAttr ".tk[116]" -type "float3" -0.077135205 0.048362095 0.01164923 ;
	setAttr ".tk[117]" -type "float3" 0.013467786 0.023339484 0.036097199 ;
	setAttr ".tk[118]" -type "float3" -0.17556302 -0.05457481 -0.0019071156 ;
	setAttr ".tk[119]" -type "float3" -1.8626451e-009 0 -2.7939677e-009 ;
	setAttr ".tk[120]" -type "float3" -0.0033317723 0.15037398 0.072032556 ;
	setAttr ".tk[121]" -type "float3" 0 -3.7252903e-009 3.7252903e-009 ;
	setAttr ".tk[122]" -type "float3" -0.071796142 -0.10416095 0.0010583586 ;
	setAttr ".tk[123]" -type "float3" 0.16233282 0.055487834 -0.026036886 ;
	setAttr ".tk[124]" -type "float3" -9.3132257e-010 1.8626451e-009 6.0535967e-009 ;
	setAttr ".tk[125]" -type "float3" -3.7252903e-009 3.7252903e-009 0 ;
	setAttr ".tk[126]" -type "float3" -1.1641532e-010 0 -3.7252903e-009 ;
	setAttr ".tk[127]" -type "float3" 9.3132257e-010 -7.4505806e-009 -3.7252903e-009 ;
	setAttr ".tk[128]" -type "float3" 1.4901161e-008 -9.3132257e-010 7.4505806e-009 ;
	setAttr ".tk[129]" -type "float3" 7.4505806e-009 -7.4505806e-009 0 ;
	setAttr ".tk[130]" -type "float3" -5.5879354e-009 1.8626451e-009 0 ;
	setAttr ".tk[131]" -type "float3" 3.7252903e-009 1.8626451e-009 -3.7252903e-009 ;
	setAttr ".tk[132]" -type "float3" 2.3283064e-010 3.7252903e-009 0 ;
	setAttr ".tk[133]" -type "float3" 3.7252903e-009 0 -3.7252903e-009 ;
	setAttr ".tk[134]" -type "float3" 0 3.7252903e-009 -7.4505806e-009 ;
	setAttr ".tk[135]" -type "float3" -3.7252903e-009 -3.7252903e-009 -1.8626451e-009 ;
createNode deleteComponent -n "deleteComponent10";
	rename -uid "F1FDFDC9-4336-CD1D-B30D-18AB9A1B9162";
	setAttr ".dc" -type "componentList" 2 "f[56]" "f[95:99]";
createNode deleteComponent -n "deleteComponent11";
	rename -uid "032546A0-4324-C0B6-70A9-649702F2ABD6";
	setAttr ".dc" -type "componentList" 2 "f[57]" "f[94:98]";
createNode polyCloseBorder -n "polyCloseBorder1";
	rename -uid "3CC0463F-4E17-13CE-90F6-F6A961A6925D";
	setAttr ".ics" -type "componentList" 4 "e[62]" "e[126]" "e[129:130]" "e[178]";
createNode polyCloseBorder -n "polyCloseBorder2";
	rename -uid "C5524DCD-48FC-A231-FB2B-3982C1CEA2CF";
	setAttr ".ics" -type "componentList" 4 "e[36]" "e[120]" "e[124:125]" "e[189]";
createNode polyExtrudeFace -n "polyExtrudeFace8";
	rename -uid "31E8D116-4E36-9C1A-ADBE-058106EC4011";
	setAttr ".ics" -type "componentList" 4 "f[52]" "f[76]" "f[91]" "f[102]";
	setAttr ".ix" -type "matrix" -0.80507248138930487 0.54452990804635171 -0.23525619854256533 0
		 0.52267126770053829 0.83874142573443433 0.15273364618603327 0 0.28048715769171911 -2.6229018956769323e-014 -0.9598577781994686 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.075247064 -0.095350832 0.54757589 ;
	setAttr ".rs" 50406;
	setAttr ".lt" -type "double3" 8.3266726846886741e-017 2.7755575615628914e-017 -0.087802764700565167 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.36135950492932201 -0.40201032916080048 0.41412184269337016 ;
	setAttr ".cbx" -type "double3" 0.57090642088686172 0.021720660190942435 0.77732782129991729 ;
createNode polyTweak -n "polyTweak16";
	rename -uid "024F4508-4CE0-D249-AF43-CE8BB3391127";
	setAttr ".uopa" yes;
	setAttr -s 118 ".tk[2:117]" -type "float3"  0.058357421 -0.036630433 -0.020131784
		 0.11187202 0.094554402 0.0032436969 0 0 0 0.064241983 0.065928861 0.044317421 0.031911131
		 0.046956904 -0.049921554 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 -0.035835575 0.038194794 0.014860693 -0.028350569 0.017768163
		 0.0097758686 -0.038524747 -0.022396801 0.0058784126 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
		 0 0 0 0 0 0 -0.061917499 -0.009550455 -0.00020375149 -0.0020918294 0.088167749 0.018607579
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.022299826 0.080508359 -0.021270139 0
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
		 0.081167042 0.041863881 -0.019637534 0 0 0 0 0 0 0 0 0 0.058357421 -0.036630433 -0.020131784
		 0.049110968 0.078146659 -0.0060074064 -0.03279727 0.041388635 0.014624258 -0.0031334786
		 -0.0048510991 0.051127858 0 0 0 0.023595996 0.026679505 0.013671191 -0.035275944
		 -0.069352113 -0.041754484 -0.038524747 -0.022396801 0.0058784126 0.019025607 0.0056773569
		 0.082739674 0 0 0 0 0 0 0 0 0 0.05957685 0.049281243 -0.04781986 0 0 0 0 0 0 0 0
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.085157171 0.046675052
		 -0.031459741 -0.022413662 0.081181668 -0.040525652 0 0 0 0.041045714 0.028756417
		 0.040641811 0 0 0 0 0 0 -0.028350569 0.017768163 0.0097758686 0 0 0 -0.089852676
		 -0.025581485 -0.24425922 -0.040722985 -0.078666762 0.0025278768 0.0058793975 0.03259474
		 -0.024039216 0.0081848539 0.08080516 -0.0010703761 0.0039949035 0.07893078 0.032235634
		 -0.12409024 -0.065795995 -0.1176555 0.022499746 0.033739511 0.041668974 -0.12373842
		 0.044724498 -0.23138465 0.065382741 0.038155388 -0.039761513 0.040494606 0.03731646
		 -0.047985449 0.05476192 0.032874789 0.035399105 -0.077126369 -0.083835639 -0.068132468
		 0.053687502 0.00057359808 -0.0072787208 0.038689926 0.071057178 0.0018239989 0.038689926
		 0.071057178 0.0018239989 -0.0480408 0.029087571 0.016402969 -0.09273614 -0.03271864
		 0.0036632363 -0.045305997 0.031962343 0.016190153 -0.0016557175 -0.0099256439 0.085325815
		 0.1152504 0.097754501 0.00050230511 0.031322692 0.034946464 0.011797288 -0.033663433
		 -0.065724738 -0.033326969 0.004427542 0.0047766715 0.056425415 -6.9849193e-010 7.4505806e-009
		 -7.4505806e-009 -6.9849193e-010 7.4505806e-009 -7.4505806e-009;
createNode polyReduce -n "polyReduce11";
	rename -uid "BB9E49BA-4E0A-76F1-DB7F-349F8C3151DC";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".ver" 1;
	setAttr ".p" 74.526800000000009;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polySmoothFace -n "polySmoothFace5";
	rename -uid "04DB7267-45F9-381C-73C7-728AB523CD5C";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".sdt" 2;
	setAttr ".ovb" 2;
	setAttr ".suv" yes;
	setAttr ".ps" 0.10000000149011612;
	setAttr ".ro" 1;
	setAttr ".ma" yes;
	setAttr ".m08" yes;
createNode polyReduce -n "polyReduce12";
	rename -uid "95DA90CC-4117-B6BA-2D89-11A28EBE5283";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".ver" 1;
	setAttr ".p" 21.1268;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polyReduce -n "polyReduce13";
	rename -uid "9C50C93D-47C3-34EC-F156-C4988CDAA7A2";
	setAttr ".ics" -type "componentList" 1 "f[0:299]";
	setAttr ".ver" 1;
	setAttr ".p" 40.4268;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polySubdFace -n "polySubdFace4";
	rename -uid "D1259C3E-4C87-5135-1E28-C2A447849547";
	setAttr ".ics" -type "componentList" 1 "f[*]";
createNode polyReduce -n "polyReduce14";
	rename -uid "E341C3FB-4D1B-A290-170C-4E86D95EACA7";
	setAttr ".ics" -type "componentList" 1 "f[*]";
	setAttr ".ver" 1;
	setAttr ".p" 66.526800000000009;
	setAttr ".tct" 1000;
	setAttr ".vmp" -type "string" "";
	setAttr ".sym" -type "double4" 0 1 0 0 ;
	setAttr ".stl" 0.01;
	setAttr ".kqw" 1;
	setAttr ".cr" yes;
createNode polyTweak -n "polyTweak17";
	rename -uid "3B5497B5-4EBC-8E37-3F7B-E2AB9F880A96";
	setAttr ".uopa" yes;
	setAttr -s 146 ".tk";
	setAttr ".tk[0]" -type "float3" 0.0096652145 -0.047918636 -0.016478423 ;
	setAttr ".tk[1]" -type "float3" -0.00015606526 0.00077374739 0.00026607868 ;
	setAttr ".tk[2]" -type "float3" 0.026828287 -0.16901201 -0.056461859 ;
	setAttr ".tk[4]" -type "float3" 0.00059559173 -0.0029528541 -0.0010154368 ;
	setAttr ".tk[5]" -type "float3" -5.6477438e-005 0.00028001191 9.6290896e-005 ;
	setAttr ".tk[8]" -type "float3" 0.025459822 -0.17301981 -0.059530817 ;
	setAttr ".tk[9]" -type "float3" 0.020184521 -0.1327195 -0.050341744 ;
	setAttr ".tk[10]" -type "float3" -0.0013885596 0.0068842634 0.002367384 ;
	setAttr ".tk[11]" -type "float3" 0.0016543696 -0.0082021039 -0.0028205665 ;
	setAttr ".tk[12]" -type "float3" -0.0010213801 0.0050638453 0.0017413718 ;
	setAttr ".tk[14]" -type "float3" -0.0015795768 0.0078312941 0.0026930524 ;
	setAttr ".tk[15]" -type "float3" -0.0011711062 0.0058061685 0.001996642 ;
	setAttr ".tk[16]" -type "float3" -0.029528283 -0.0073002153 0.060998917 ;
	setAttr ".tk[17]" -type "float3" 0.022146445 -0.17477818 -0.06158736 ;
	setAttr ".tk[18]" -type "float3" 0.017140206 -0.16142216 -0.061567459 ;
	setAttr ".tk[19]" -type "float3" 0.017616656 -0.15118448 -0.060194358 ;
	setAttr ".tk[20]" -type "float3" 0.024152132 -0.17102739 -0.058041491 ;
	setAttr ".tk[21]" -type "float3" -6.0766237e-005 0.00030126941 0.00010360157 ;
	setAttr ".tk[22]" -type "float3" 0.015968997 -0.16922873 -0.064632848 ;
	setAttr ".tk[23]" -type "float3" -0.00098235882 0.0048703793 0.0016748429 ;
	setAttr ".tk[24]" -type "float3" 0.0043148054 -0.021392129 -0.0073563964 ;
	setAttr ".tk[27]" -type "float3" 0.0084783975 -0.042034578 -0.014454992 ;
	setAttr ".tk[30]" -type "float3" 0.012094045 -0.11051626 -0.046638198 ;
	setAttr ".tk[31]" -type "float3" 0.018756095 -0.17317116 -0.063494675 ;
	setAttr ".tk[32]" -type "float3" 0.11233157 -0.081000984 0.0332557 ;
	setAttr ".tk[33]" -type "float3" 0.076917991 -0.090571508 0.12228288 ;
	setAttr ".tk[34]" -type "float3" 0.10552125 -0.079426572 0.04544941 ;
	setAttr ".tk[35]" -type "float3" 0.020087708 -0.14287236 -0.05480843 ;
	setAttr ".tk[36]" -type "float3" 0.01172059 -0.06526906 -0.031565178 ;
	setAttr ".tk[37]" -type "float3" 0.012629419 -0.062614702 -0.021532156 ;
	setAttr ".tk[38]" -type "float3" -0.00066493294 0.0032966333 0.0011336576 ;
	setAttr ".tk[39]" -type "float3" -0.0017056054 0.0084561249 0.002907922 ;
	setAttr ".tk[40]" -type "float3" -0.0014317186 0.0070982384 0.002440966 ;
	setAttr ".tk[41]" -type "float3" -0.0011735909 0.0058184825 0.0020008802 ;
	setAttr ".tk[42]" -type "float3" -0.0019559348 0.0096972212 0.0033347118 ;
	setAttr ".tk[43]" -type "float3" -0.00039051811 0.0019361272 0.00066580216 ;
	setAttr ".tk[44]" -type "float3" -0.00049370382 0.002447699 0.00084172282 ;
	setAttr ".tk[45]" -type "float3" 0.00055223249 -0.0027378872 -0.0009415102 ;
	setAttr ".tk[46]" -type "float3" -0.022237502 0.072155446 -0.020670056 ;
	setAttr ".tk[47]" -type "float3" 0.0089277886 -0.044262595 -0.015221171 ;
	setAttr ".tk[48]" -type "float3" 0.0069560753 -0.034487136 -0.011859553 ;
	setAttr ".tk[49]" -type "float3" 0.0062443814 -0.030958667 -0.010646171 ;
	setAttr ".tk[50]" -type "float3" 0.043103438 -0.017309939 0.010337902 ;
	setAttr ".tk[51]" -type "float3" 0.011026647 -0.0546684 -0.018799551 ;
	setAttr ".tk[52]" -type "float3" 0.0014431719 -0.0071550151 -0.0024604893 ;
	setAttr ".tk[53]" -type "float3" 0.0015869385 -0.0078677982 -0.002705605 ;
	setAttr ".tk[54]" -type "float3" -0.00010211242 0.00050626043 0.00017409504 ;
	setAttr ".tk[56]" -type "float3" -0.0010302912 0.0051080324 0.0017565626 ;
	setAttr ".tk[57]" -type "float3" 0.0020097243 -0.0099638999 -0.00342642 ;
	setAttr ".tk[58]" -type "float3" -0.0016267118 0.0080649797 0.0027734137 ;
	setAttr ".tk[59]" -type "float3" 0.0011852912 -0.0058764983 -0.0020208298 ;
	setAttr ".tk[60]" -type "float3" -0.00025399693 0.0012592734 0.00043304241 ;
	setAttr ".tk[62]" -type "float3" 0.0028631166 -0.014194896 -0.0048813871 ;
	setAttr ".tk[63]" -type "float3" 0.0053692367 -0.026619833 -0.0091541223 ;
	setAttr ".tk[66]" -type "float3" -0.00099135411 0.0049149729 0.0016901804 ;
	setAttr ".tk[67]" -type "float3" 0.0040404843 -0.020032097 -0.0068887053 ;
	setAttr ".tk[68]" -type "float3" -0.00090214552 0.0044726995 0.0015380869 ;
	setAttr ".tk[69]" -type "float3" -0.00025310239 0.0012548428 0.00043151941 ;
	setAttr ".tk[70]" -type "float3" -0.00043288921 0.0021461968 0.00073804159 ;
	setAttr ".tk[71]" -type "float3" -0.00024808789 0.001229981 0.00042296835 ;
	setAttr ".tk[76]" -type "float3" -5.3353411e-005 0.00026451773 9.0963265e-005 ;
	setAttr ".tk[77]" -type "float3" -0.00073088653 0.0036236208 0.0012461029 ;
	setAttr ".tk[78]" -type "float3" -0.0012573866 0.0062339301 0.0021437444 ;
	setAttr ".tk[79]" -type "float3" -0.00027822229 0.0013793859 0.00047434776 ;
	setAttr ".tk[80]" -type "float3" 0.00064428645 -0.0031942732 -0.0010984596 ;
	setAttr ".tk[81]" -type "float3" 0.0056635533 -0.028079018 -0.0096559059 ;
	setAttr ".tk[82]" -type "float3" 0.0005336164 -0.0026455875 -0.00090977445 ;
	setAttr ".tk[83]" -type "float3" 0.019361958 -0.10119666 -0.037092857 ;
	setAttr ".tk[84]" -type "float3" 0.061206982 -0.046329599 0.0077526998 ;
	setAttr ".tk[85]" -type "float3" 0.016996723 -0.07273069 -0.027596874 ;
	setAttr ".tk[86]" -type "float3" 0.026225338 -0.17208722 -0.058447089 ;
	setAttr ".tk[87]" -type "float3" 0.12178027 -0.082389146 0.042522803 ;
	setAttr ".tk[88]" -type "float3" 0.0023503583 -0.011652716 -0.0040071737 ;
	setAttr ".tk[89]" -type "float3" 0.0048432034 -0.024011858 -0.0082572782 ;
	setAttr ".tk[90]" -type "float3" -0.01688347 -0.061537772 -0.051606707 ;
	setAttr ".tk[91]" -type "float3" 0.016916092 -0.17015983 -0.063939914 ;
	setAttr ".tk[92]" -type "float3" 0.10690068 -0.093849935 -0.042520296 ;
	setAttr ".tk[93]" -type "float3" 0.083551541 -0.10434168 0.015146704 ;
	setAttr ".tk[94]" -type "float3" -0.19107072 0.083683185 -0.09894 ;
	setAttr ".tk[95]" -type "float3" 0.046972789 -0.068349428 0.021134542 ;
	setAttr ".tk[96]" -type "float3" 0.011953404 -0.059263121 -0.020379603 ;
	setAttr ".tk[97]" -type "float3" 0.005439647 -0.026968934 -0.009274167 ;
	setAttr ".tk[98]" -type "float3" 0.0068068933 -0.03374752 -0.01160521 ;
	setAttr ".tk[99]" -type "float3" 0.019753203 -0.15364206 -0.055414375 ;
	setAttr ".tk[100]" -type "float3" 0.019515663 -0.17026024 -0.061059322 ;
	setAttr ".tk[101]" -type "float3" 0.023126494 -0.17321829 -0.060010284 ;
	setAttr ".tk[102]" -type "float3" 0.015243947 -0.074963197 -0.029081376 ;
	setAttr ".tk[249]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[250]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[252]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[259]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[265]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[266]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[268]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[279]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[287]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[296]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[298]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[301]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[302]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[304]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[306]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[310]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[312]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[314]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[315]" -type "float3" 0 1.1920929e-007 0 ;
	setAttr ".tk[331]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[355]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[358]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[360]" -type "float3" 2.3841858e-007 5.9604645e-008 0 ;
	setAttr ".tk[364]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[366]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[367]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[368]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[370]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[371]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[380]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[387]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[389]" -type "float3" 0 1.4901161e-008 0 ;
	setAttr ".tk[392]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[393]" -type "float3" 0 1.4901161e-008 0 ;
	setAttr ".tk[394]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[396]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[398]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[399]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[403]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[405]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[413]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[421]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[422]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[429]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[437]" -type "float3" 0 5.9604645e-008 0 ;
	setAttr ".tk[438]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[441]" -type "float3" 0 1.4901161e-008 0 ;
	setAttr ".tk[447]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[449]" -type "float3" 4.7683716e-007 0 0 ;
	setAttr ".tk[460]" -type "float3" 0 1.4901161e-008 0 ;
	setAttr ".tk[462]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[470]" -type "float3" 2.3841858e-007 2.9802322e-008 0 ;
	setAttr ".tk[472]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[475]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[477]" -type "float3" 2.3841858e-007 0 0 ;
	setAttr ".tk[478]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[479]" -type "float3" 0 2.9802322e-008 0 ;
	setAttr ".tk[483]" -type "float3" 2.3841858e-007 0 0 ;
createNode polySplit -n "polySplit19";
	rename -uid "E35F4F67-42ED-3528-9AE6-51937C03C279";
	setAttr -s 3 ".e[0:2]"  0 0.5 0;
	setAttr -s 3 ".d[0:2]"  -2147483628 -2147483621 -2147483623;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
	setAttr ".ief" yes;
createNode polySplit -n "polySplit20";
	rename -uid "25884696-4940-80D6-2570-9B9727AC27B3";
	setAttr -s 3 ".e[0:2]"  0.5 0.5 1;
	setAttr -s 3 ".d[0:2]"  -2147483644 -2147483627 -2147483605;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
	setAttr ".ief" yes;
createNode polySplit -n "polySplit21";
	rename -uid "7BA4C8A4-45F2-08A2-4B21-8FBE74BA257A";
	setAttr -s 3 ".e[0:2]"  0 0.5 0.5;
	setAttr -s 3 ".d[0:2]"  -2147483469 -2147483625 -2147483641;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
	setAttr ".ief" yes;
createNode polySplit -n "polySplit22";
	rename -uid "14CBC309-4538-68F9-1033-D1B6CE5D32B6";
	setAttr -s 5 ".e[0:4]"  0 0.5 0.5 0.5 1;
	setAttr -s 5 ".d[0:4]"  -2147483578 -2147483570 -2147483642 -2147483469 -2147483470;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
	setAttr ".ief" yes;
createNode polyExtrudeFace -n "polyExtrudeFace9";
	rename -uid "5DCE2577-4D05-0109-23CF-849C4CB646E5";
	setAttr ".ics" -type "componentList" 4 "f[78]" "f[83]" "f[108]" "f[111]";
	setAttr ".ix" -type "matrix" -0.55731489171754722 0.75056235016289163 -0.35504403950470098 0
		 -0.36830905817812132 0.15976201842466575 0.9158736458337593 0 0.74414282845015989 0.6411959575565287 0.18740116029353238 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.016553013 0.085152134 0.43273023 ;
	setAttr ".rs" 53247;
	setAttr ".lt" -type "double3" -6.9388939039072284e-018 1.0408340855860843e-017 -0.1196481815909831 ;
	setAttr ".ls" -type "double3" 0.65000000007369807 0.65000000007369807 0.65000000007369807 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.35286327464809264 -0.021950410771827841 0.38721666964578422 ;
	setAttr ".cbx" -type "double3" 0.4019280830555505 0.30747956325917886 0.55464512862747017 ;
createNode polyTweak -n "polyTweak18";
	rename -uid "32C77671-4821-EF8F-75A2-3ABF7E6C3DE1";
	setAttr ".uopa" yes;
	setAttr -s 27 ".tk";
	setAttr ".tk[8]" -type "float3" -5.9604645e-008 -5.9604645e-008 -5.5879354e-008 ;
	setAttr ".tk[16]" -type "float3" 0 -5.5879354e-009 -7.4505806e-009 ;
	setAttr ".tk[17]" -type "float3" 2.5331974e-007 5.2154064e-008 3.7252903e-008 ;
	setAttr ".tk[18]" -type "float3" 2.2351742e-008 -3.7252903e-009 2.9802322e-008 ;
	setAttr ".tk[19]" -type "float3" 6.7986548e-008 1.8626451e-009 -1.4901161e-007 ;
	setAttr ".tk[31]" -type "float3" 3.1664968e-008 -2.2351742e-008 1.6391277e-007 ;
	setAttr ".tk[32]" -type "float3" 8.9406967e-008 7.4505806e-008 2.9802322e-008 ;
	setAttr ".tk[33]" -type "float3" -4.4703484e-008 -2.9802322e-008 3.259629e-009 ;
	setAttr ".tk[34]" -type "float3" 0.072003603 0.031991184 -0.019932259 ;
	setAttr ".tk[46]" -type "float3" -7.4505806e-009 -1.8626451e-009 7.4505806e-009 ;
	setAttr ".tk[50]" -type "float3" 0 2.7939677e-008 0 ;
	setAttr ".tk[87]" -type "float3" -3.7252903e-008 0 -5.9604645e-008 ;
	setAttr ".tk[91]" -type "float3" -1.3411045e-007 -1.1175871e-007 0 ;
	setAttr ".tk[92]" -type "float3" -1.8626451e-008 0 2.9802322e-008 ;
	setAttr ".tk[93]" -type "float3" -8.1956387e-008 -5.5879354e-009 -2.0861626e-007 ;
	setAttr ".tk[94]" -type "float3" 2.9802322e-008 -1.8626451e-009 -7.4505806e-009 ;
	setAttr ".tk[95]" -type "float3" 0.079771571 0.029316677 0.0078550596 ;
	setAttr ".tk[104]" -type "float3" -9.3132257e-010 9.3132257e-010 0 ;
	setAttr ".tk[105]" -type "float3" -1.8626451e-009 0 1.8626451e-009 ;
	setAttr ".tk[106]" -type "float3" 1.8626451e-009 -9.3132257e-010 3.7252903e-009 ;
	setAttr ".tk[107]" -type "float3" 0.072003707 0.031991102 -0.019932244 ;
	setAttr ".tk[108]" -type "float3" 0 -4.6566129e-010 0 ;
	setAttr ".tk[109]" -type "float3" 0 -1.8626451e-009 9.3132257e-010 ;
	setAttr ".tk[110]" -type "float3" 0 0 -1.8626451e-009 ;
	setAttr ".tk[111]" -type "float3" 9.3132257e-010 9.3132257e-010 0 ;
createNode polyExtrudeFace -n "polyExtrudeFace10";
	rename -uid "93846AB6-4782-6B78-A96A-8EA91E34B74B";
	setAttr ".ics" -type "componentList" 4 "f[17]" "f[20]" "f[79]" "f[82]";
	setAttr ".ix" -type "matrix" -0.55731489171754722 0.75056235016289163 -0.35504403950470098 0
		 -0.36830905817812132 0.15976201842466575 0.9158736458337593 0 0.74414282845015989 0.6411959575565287 0.18740116029353238 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -0.021907561 0.18485615 0.31846765 ;
	setAttr ".rs" 64994;
	setAttr ".lt" -type "double3" 9.0205620750793969e-017 3.4694469519536142e-017 0.10364075264733358 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.47218977784276267 0.074140263374566237 0.24643458893120285 ;
	setAttr ".cbx" -type "double3" 0.54079282462098321 0.60687408979102553 0.49077314283132734 ;
createNode polyExtrudeFace -n "polyExtrudeFace11";
	rename -uid "0D8B94CD-4854-9409-AD79-4D9C34B6F36D";
	setAttr ".ics" -type "componentList" 4 "f[24]" "f[91]" "f[95:96]" "f[99]";
	setAttr ".ix" -type "matrix" -0.55731489171754722 0.75056235016289163 -0.35504403950470098 0
		 -0.36830905817812132 0.15976201842466575 0.9158736458337593 0 0.74414282845015989 0.6411959575565287 0.18740116029353238 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.019346025 -0.18534635 0.46625891 ;
	setAttr ".rs" 50998;
	setAttr ".lt" -type "double3" -6.9388939039072284e-018 6.2450045135165055e-017 -0.11661776708098961 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.18561758425357364 -0.32334525179459145 0.41358616155582356 ;
	setAttr ".cbx" -type "double3" 0.25903079193650669 -0.0085454567457063889 0.53027224398268036 ;
createNode polyTweak -n "polyTweak19";
	rename -uid "E2816DDF-4B06-18F8-4814-50977C22BAA9";
	setAttr ".uopa" yes;
	setAttr -s 58 ".tk";
	setAttr ".tk[2]" -type "float3" 0.022670127 -0.0013305225 0.035629839 ;
	setAttr ".tk[8]" -type "float3" 0.026526278 0.00058135588 0.051933121 ;
	setAttr ".tk[9]" -type "float3" 0.072433785 0.0088372556 0.041427467 ;
	setAttr ".tk[16]" -type "float3" -0.038674086 0.022002734 -0.087184258 ;
	setAttr ".tk[17]" -type "float3" 0.036627647 0.016687142 0.028718572 ;
	setAttr ".tk[18]" -type "float3" 0.025401052 0.011963585 0.038403105 ;
	setAttr ".tk[19]" -type "float3" 0.054667287 0.021507869 0.018638402 ;
	setAttr ".tk[20]" -type "float3" 0.064613625 0.014816121 0.087966338 ;
	setAttr ".tk[22]" -type "float3" -0.0072177956 0.0037185983 -0.031848263 ;
	setAttr ".tk[31]" -type "float3" 0.090645634 0.052564181 0.010582086 ;
	setAttr ".tk[32]" -type "float3" -0.087926574 0.012598461 -0.042093761 ;
	setAttr ".tk[33]" -type "float3" -0.028022286 -0.00055137835 -0.062559269 ;
	setAttr ".tk[34]" -type "float3" -0.068701312 -0.027906658 0.026982334 ;
	setAttr ".tk[35]" -type "float3" 0.043845855 0.013417877 0.011342579 ;
	setAttr ".tk[46]" -type "float3" -0.043215796 0.0027676753 -0.061599676 ;
	setAttr ".tk[50]" -type "float3" -0.10345084 0.0096359923 -0.046792369 ;
	setAttr ".tk[84]" -type "float3" 0.026882328 0.015635194 -0.094771124 ;
	setAttr ".tk[86]" -type "float3" 0.024057074 0.00049497694 0.046712585 ;
	setAttr ".tk[87]" -type "float3" 0.0095770461 0.018262001 -0.077096738 ;
	setAttr ".tk[91]" -type "float3" -0.00080664875 -0.016213689 0.079962544 ;
	setAttr ".tk[92]" -type "float3" -0.061998393 -0.021826793 -0.033574246 ;
	setAttr ".tk[93]" -type "float3" -0.029770015 -0.0031001423 -0.049486578 ;
	setAttr ".tk[94]" -type "float3" -0.098890036 -0.022303293 -0.011413217 ;
	setAttr ".tk[95]" -type "float3" -0.078388877 -0.043851975 0.045972969 ;
	setAttr ".tk[99]" -type "float3" 0.081190087 0.025540242 0.066180438 ;
	setAttr ".tk[100]" -type "float3" -0.031330131 -0.0074149347 0.007613631 ;
	setAttr ".tk[101]" -type "float3" 0.041290153 0.015719045 0.040931202 ;
	setAttr ".tk[103]" -type "float3" 0.022280741 -0.00181868 0.02894925 ;
	setAttr ".tk[104]" -type "float3" -0.0030399493 0.017932689 -0.069387853 ;
	setAttr ".tk[105]" -type "float3" -0.011475392 0.019925898 -0.057950418 ;
	setAttr ".tk[106]" -type "float3" -0.047834203 -0.0069562327 0.0018024482 ;
	setAttr ".tk[107]" -type "float3" -0.064886071 -0.020422626 0.0155438 ;
	setAttr ".tk[108]" -type "float3" 0.040371336 0.015322533 0.016656268 ;
	setAttr ".tk[110]" -type "float3" -0.013399944 -0.0010242832 -0.0013273878 ;
	setAttr ".tk[111]" -type "float3" -0.0074702231 0.0047329417 -0.059241358 ;
	setAttr ".tk[112]" -type "float3" -0.013488114 0.0079309791 -0.054075707 ;
	setAttr ".tk[113]" -type "float3" -0.00079409406 -0.0045357738 -0.046477191 ;
	setAttr ".tk[114]" -type "float3" 0.00073082309 0.0049469844 -0.064252146 ;
	setAttr ".tk[115]" -type "float3" 0.037718594 0.018567871 0.0089172013 ;
	setAttr ".tk[116]" -type "float3" 0.05334606 0.011593594 0.016170267 ;
	setAttr ".tk[117]" -type "float3" -0.040967643 -0.013658406 -0.0055539743 ;
	setAttr ".tk[118]" -type "float3" -0.05397892 -0.023907641 0.0052531417 ;
	setAttr ".tk[119]" -type "float3" -0.056458812 -0.028772261 0.012688186 ;
	setAttr ".tk[120]" -type "float3" -0.030060798 -0.0094570704 -0.021476164 ;
	setAttr ".tk[121]" -type "float3" -0.0096190087 -0.020435276 0.028761903 ;
	setAttr ".tk[122]" -type "float3" -0.014810449 -0.003687012 0.00040658255 ;
	setAttr ".tk[123]" -type "float3" -0.0019421121 -0.0041420595 0.014090588 ;
	setAttr ".tk[124]" -type "float3" -0.0065954691 -0.024455728 -0.04964174 ;
	setAttr ".tk[125]" -type "float3" -0.045137383 0.005301916 -0.057008944 ;
	setAttr ".tk[126]" -type "float3" -0.012156833 0.008219596 0.01728511 ;
	setAttr ".tk[127]" -type "float3" -0.093984187 -0.042885523 -0.011041453 ;
	setAttr ".tk[128]" -type "float3" -0.14848939 -0.012691792 -0.043084357 ;
	setAttr ".tk[129]" -type "float3" 0.0063167275 -0.011981633 -0.07766366 ;
	setAttr ".tk[130]" -type "float3" -0.021183858 -0.030118244 -0.067212403 ;
	setAttr ".tk[131]" -type "float3" -0.089516841 -0.011120524 -0.084470198 ;
	setAttr ".tk[132]" -type "float3" -0.061600886 -0.00048762621 -0.088635169 ;
	setAttr ".tk[133]" -type "float3" 0.014161777 -0.0023111198 -0.007358944 ;
	setAttr ".tk[134]" -type "float3" 0.0076871365 0.0072154738 -0.012992966 ;
createNode polySplit -n "polySplit23";
	rename -uid "0F7666BD-452E-FD04-D3A4-13AE6A30ACEA";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483637 -2147483550;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyTweak -n "polyTweak20";
	rename -uid "F567B5C1-4071-A2F3-ACBD-EE93B351B342";
	setAttr ".uopa" yes;
	setAttr -s 30 ".tk";
	setAttr ".tk[1]" -type "float3" 0.13485612 0.028705027 0.1152059 ;
	setAttr ".tk[11]" -type "float3" -0.023357093 -0.004971676 -0.019953674 ;
	setAttr ".tk[13]" -type "float3" 0.14657374 0.031199176 0.12521607 ;
	setAttr ".tk[21]" -type "float3" 0.068852201 0.014655691 0.058819715 ;
	setAttr ".tk[24]" -type "float3" -0.083585069 -0.017791586 -0.071405649 ;
	setAttr ".tk[36]" -type "float3" 0.0062379437 0.0013278138 0.0053290585 ;
	setAttr ".tk[47]" -type "float3" -0.037323233 -0.0079444787 -0.0318847 ;
	setAttr ".tk[48]" -type "float3" -0.066621289 -0.01418085 -0.056913711 ;
	setAttr ".tk[49]" -type "float3" -0.042662479 -0.0090809753 -0.036446039 ;
	setAttr ".tk[57]" -type "float3" -0.080686554 -0.017174648 -0.068929516 ;
	setAttr ".tk[59]" -type "float3" 0.024782477 0.0052751307 0.021171341 ;
	setAttr ".tk[60]" -type "float3" 0.035765067 0.00761283 0.030553587 ;
	setAttr ".tk[62]" -type "float3" 0.028883388 0.0061480389 0.024674729 ;
	setAttr ".tk[72]" -type "float3" 0.072703287 0.015475395 0.062109623 ;
	setAttr ".tk[73]" -type "float3" -0.015719526 -0.0033460036 -0.013429001 ;
	setAttr ".tk[74]" -type "float3" 0.10431869 0.022204895 0.089118049 ;
	setAttr ".tk[79]" -type "float3" 0.10080282 0.021456551 0.08611463 ;
	setAttr ".tk[82]" -type "float3" 0.10223567 0.021761525 0.087338701 ;
	setAttr ".tk[85]" -type "float3" 0.026686851 0.0056804908 0.022798354 ;
	setAttr ".tk[102]" -type "float3" 0.015586113 0.003317642 0.013315136 ;
	setAttr ".tk[135]" -type "float3" -0.0017388812 0.0022578025 0.013796576 ;
	setAttr ".tk[136]" -type "float3" 0.0062751644 0.01833803 -0.0042459778 ;
	setAttr ".tk[137]" -type "float3" -0.061587062 -0.027459592 -0.049029868 ;
	setAttr ".tk[138]" -type "float3" -0.012850862 0.0012163791 -0.034748387 ;
	setAttr ".tk[139]" -type "float3" -0.059656192 -0.023706835 -0.065143816 ;
	setAttr ".tk[140]" -type "float3" -0.016377633 -0.0038143513 0.0026996792 ;
	setAttr ".tk[141]" -type "float3" -0.069971159 -0.030396316 -0.052553587 ;
	setAttr ".tk[142]" -type "float3" -0.047482107 -0.017138679 -0.020538744 ;
	setAttr ".tk[143]" -type "float3" -0.088816598 -0.035912838 -0.066242188 ;
	setAttr ".tk[144]" -type "float3" -0.066333622 -0.022096563 -0.034890212 ;
createNode polySplit -n "polySplit24";
	rename -uid "3AFD5F3E-44E1-FF3B-4E56-1682DF2D5A4B";
	setAttr -s 15 ".e[0:14]"  0.5 0.33853701 0.51102602 0.5 0.56134498
		 0.42064601 0.5 0.5 0.5 0.5 0.5 0.5 0.5 0.5 0.5;
	setAttr -s 15 ".d[0:14]"  -2147483551 -2147483556 -2147483526 -2147483527 -2147483618 -2147483534 
		-2147483532 -2147483619 -2147483540 -2147483548 -2147483547 -2147483359 -2147483637 -2147483554 -2147483551;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
	setAttr ".ief" yes;
createNode createColorSet -n "createColorSet3";
	rename -uid "0CC03279-4EFE-5804-854F-31849824570C";
	setAttr ".colos" -type "string" "SculptFreezeColorTemp";
	setAttr ".clam" no;
createNode createColorSet -n "createColorSet4";
	rename -uid "2B834F00-47F6-E588-951B-558ABB9C5138";
	setAttr ".colos" -type "string" "SculptMaskColorTemp";
	setAttr ".clam" no;
createNode polySplit -n "polySplit25";
	rename -uid "363C984F-4781-D414-57A8-969989851F11";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483623 -2147483449;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyTweak -n "polyTweak21";
	rename -uid "095AB787-483C-D40A-B13A-E9A8E90DD0BA";
	setAttr ".uopa" yes;
	setAttr -s 159 ".tk[1:158]" -type "float3"  0.0090269819 0.012297869 0.011237085
		 -0.14617035 -0.05239049 0.11201034 -0.075134814 0.20661235 0.0016516447 -0.032042325
		 0.012557343 0.0066782534 0 0 0 -0.1590686 0.18235368 0.16933881 -0.033079222 0.28847215
		 0.05483751 -0.17228946 -0.012165179 -0.051111124 0.081351914 -0.017953465 0.19933099
		 0.011025593 0.005700171 -0.006185472 0 0 0 -0.12577957 -0.008724466 0.053667843 0.0019639656
		 -0.0014069974 0.0058707595 -0.087966621 0.010735184 0.056009471 -0.051677197 -0.0033302307
		 0.0063160658 -0.062967636 -0.014692976 0.01792343 -0.10734826 0.059371799 -0.11955338
		 0 0 0 0 0 0 -0.070260651 0.04818587 -0.016629972 0 0 0 0 0 0 -0.041689336 -0.023299769
		 0.016220793 -0.022776915 0.075739838 -0.070239961 -0.092987537 0.14098841 0.01900816
		 -0.13663402 0.10467643 -0.0028902888 0 0 0 -0.12856823 0.10583591 -0.020162031 -0.10103481
		 0.23270598 -0.037037253 0 0 0 0 0 0 0 0 0 -0.037506234 -0.023977475 0.01208315 0
		 0 0 0.13094348 0.055841248 0.14277112 0 0 0 0 0 0 -0.1254971 -0.0376499 0.072761685
		 -0.096898019 -0.052719619 -0.037163913 -0.13066188 -0.09192507 -0.030418187 -0.070513546
		 -0.041243523 0.00022403896 -0.097592324 -0.015995163 0.0049268305 -0.015794814 0.011980549
		 0.023113623 -0.027148843 0.0074455142 0.0090080798 -0.0092798471 0.0024905205 0.013179034
		 0 0 0 -0.026754586 0.073111184 -0.064928859 -0.020809529 0.077039994 -0.072866969
		 -0.029238606 0.071469553 -0.061612226 0 0 0 0 0 0 1.6644597e-005 0.00027696788 5.5015087e-005
		 0 0 0 -0.0021960586 0.00012131035 -0.00068026781 -0.13127771 0.24881789 0.16875844
		 -0.010979995 -0.017523125 -0.01243034 -0.024114465 0.074856095 -0.068454288 -0.016058102
		 0.018478662 -0.027383834 -0.03852918 0.065382294 -0.049337026 -0.041452669 0.06148728
		 -0.047264677 0.026024401 0.17040971 0.03782589 -0.038797937 0.065180987 -0.048875373
		 -0.0039195418 0.0039642453 -0.0048224349 -0.040570974 0.14737514 -0.030223817 -0.099134207
		 0.12376589 -0.026465386 0.00011859834 9.9092722e-005 3.0696392e-006 0 0 0 -1.1444092e-005
		 0.00069710612 -0.001047045 0.028935969 0.025423557 -0.029389732 0.014157027 0.011787504
		 0.0029620081 0 0 0 0 0 0 0.0092799366 0.0019386709 0.0011330992 0.0076328069 0.0016784966
		 0.0014508367 0.024190307 0.012979597 0.0520702 0.034121603 0.030994654 0.050604701
		 0.0033705086 0.048090279 0.070715487 0.016052581 -0.015781756 0.031138599 0.003802076
		 -0.012757808 0.0067215264 -0.0058397651 -0.0077589154 0.0076072216 0 0 0 2.0429492e-005
		 -5.6922436e-006 8.6724758e-006 0 0 0 0.0099174706 -0.01862707 0.10982421 0 0 0 -0.17010428
		 -0.024886141 0.010837072 -0.02261379 -0.0054238103 -0.016335875 0.0021999627 5.0872564e-005
		 0.0027023852 6.8694353e-005 -0.00015363097 0.0001103878 0 0 0 0 0 0 0 0 0 0 0 0 0
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.048558138 0.093463093 -0.010315178 0 0 0 -0.098806493
		 0.047855277 -0.057289157 0 0 0 -0.12703341 -0.06246366 0.15983364 0 0 0 0 0 0 0 0
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
		 0 0 0 0 0 0 0 0 -0.015206018 -0.0021299685 -0.018399164 -0.047968391 -0.034392871
		 0.011794974 -0.069147505 -0.026214411 0.014985473 0.002509705 -0.021920919 0.11188748
		 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 -0.081546739 -0.11050742 0.1087089
		 0.1037816 -0.065022707 0.14041212 -0.038160797 -0.031136602 0.0056976411 0.15114826
		 -0.0061158398 0.10913545 0.072935455 0.0098567903 0.018897614 -0.091159299 -0.10305656
		 0.081997782 -0.057930391 -0.034569379 -0.010715401 -0.11494192 -0.087700397 0.024895081
		 -0.065935999 -0.028617771 -0.044382278 -0.11731792 -0.080589354 -0.0082235616 0.0084307157
		 0.079068169 -0.027381251 0.02302707 0.080790624 -0.008145446 0.013473859 0.074530736
		 0.0040450688 0.010343842 0.072347172 0.025460519 -0.0022803196 0.063365765 0.0022469433
		 -0.022296505 0.044184461 -0.0069928016 0.018467747 0.15003259 0.024063781 0.071460679
		 0.16215608 0.013535 0.034139667 0.089485534 -0.029586067 0.040404379 0.091610506
		 -0.028102301 0.034493562 0.090902545 -0.035840753 -0.020874036 0.07842882 -0.079776369
		 0.064985014 0.09714146 -0.0085636918 -0.0054449621 0.078624047 -0.051499166;
createNode deleteComponent -n "deleteComponent12";
	rename -uid "2CA0F66B-4AD2-BC52-85C9-E697020E094A";
	setAttr ".dc" -type "componentList" 1 "e[197]";
createNode polySplit -n "polySplit26";
	rename -uid "38C691A5-4D6E-D309-3D20-BBA48EEEF0DB";
	setAttr -s 2 ".e[0:1]"  0 1;
	setAttr -s 2 ".d[0:1]"  -2147483451 -2147483454;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode deleteComponent -n "deleteComponent13";
	rename -uid "9970539B-4467-778F-93CC-159782E43330";
	setAttr ".dc" -type "componentList" 1 "e[20]";
createNode polySplit -n "polySplit27";
	rename -uid "EFD44522-4F36-6F55-FDCD-CCB858C0651E";
	setAttr -s 2 ".e[0:1]"  0 0;
	setAttr -s 2 ".d[0:1]"  -2147483645 -2147483580;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit28";
	rename -uid "C6837F35-4841-0450-35DC-81A63E175249";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483645 -2147483580;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode deleteComponent -n "deleteComponent14";
	rename -uid "D316083C-4187-5492-5892-769872B0DD57";
	setAttr ".dc" -type "componentList" 2 "e[4]" "e[161]";
createNode polySplit -n "polySplit29";
	rename -uid "38F1C56C-4C28-5989-7CC6-AE9151C4C4B7";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483489 -2147483587;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit30";
	rename -uid "E730000A-43EC-E67B-E5D7-94B316CC9088";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483481 -2147483491;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode deleteComponent -n "deleteComponent15";
	rename -uid "3B4E403D-4BE9-98FD-F1F6-10A4F80ED742";
	setAttr ".dc" -type "componentList" 1 "e[23]";
createNode polySplit -n "polySplit31";
	rename -uid "59364506-4ED4-C337-5D9C-88A11E55649A";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483576 -2147483575;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyTweak -n "polyTweak22";
	rename -uid "01D0A0BD-451F-FD7A-FE96-E7B486AA9B4D";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk";
	setAttr ".tk[35]" -type "float3" -0.0069973911 0.00081481243 -0.013429475 ;
createNode deleteComponent -n "deleteComponent16";
	rename -uid "A8819019-4887-6B0D-B93F-06AF439C092D";
	setAttr ".dc" -type "componentList" 1 "e[74]";
createNode polySplit -n "polySplit32";
	rename -uid "F1A6E7BF-4EF9-7C5F-09DB-ECB3C0BFB31B";
	setAttr -s 2 ".e[0:1]"  1 0;
	setAttr -s 2 ".d[0:1]"  -2147483576 -2147483642;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode deleteComponent -n "deleteComponent17";
	rename -uid "77458BEB-4176-C3BF-9D67-0D888610FA75";
	setAttr ".dc" -type "componentList" 1 "e[8]";
createNode polySplit -n "polySplit33";
	rename -uid "48DAAD7A-4884-FF65-3326-1AA443ABEAB1";
	setAttr -s 3 ".e[0:2]"  0 0 1;
	setAttr -s 3 ".d[0:2]"  -2147483626 -2147483466 -2147483476;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polySplit -n "polySplit34";
	rename -uid "37E4C232-4051-7A16-F79E-C0940B3A6F53";
	setAttr -s 2 ".e[0:1]"  0 0;
	setAttr -s 2 ".d[0:1]"  -2147483630 -2147483454;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyTweak -n "polyTweak23";
	rename -uid "FB26C34E-4A17-EDD9-84A6-5CA9A65A0510";
	setAttr ".uopa" yes;
	setAttr -s 6 ".tk";
	setAttr ".tk[100]" -type "float3" 0.013923085 0.043732077 0.022346225 ;
createNode polySplit -n "polySplit35";
	rename -uid "A8A680F1-48FB-4B6C-3768-69A678E74AA9";
	setAttr -s 2 ".e[0:1]"  0 0;
	setAttr -s 2 ".d[0:1]"  -2147483494 -2147483455;
	setAttr ".sma" 180;
	setAttr ".m2015" yes;
createNode polyExtrudeFace -n "polyExtrudeFace12";
	rename -uid "44D6544A-4D60-BBCF-0458-17BE968F01EC";
	setAttr ".ics" -type "componentList" 1 "f[53]";
	setAttr ".ix" -type "matrix" -0.92810740213313181 0.5945596759751397 -0.30597306776889177 0
		 0.56466562455059632 0.97724248172816242 0.18615568953579709 0 0.3581528429964711 -1.1159972072809353e-014 -1.0863841942164962 0
		 0 2.7817231000642724 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.038874861 2.5659039 0.74029481 ;
	setAttr ".rs" 62873;
	setAttr ".lt" -type "double3" -1.214306433183765e-017 -7.4246164771807344e-016 -0.11572672587202712 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.19390685146897679 2.5069973767206997 0.70302756001216626 ;
	setAttr ".cbx" -type "double3" 0.23619688057383648 2.7065680073788521 0.78003468975377388 ;
createNode polyTweak -n "polyTweak24";
	rename -uid "4B0B94E3-4300-09F2-5C1E-AEA608619CD6";
	setAttr ".uopa" yes;
	setAttr -s 65 ".tk";
	setAttr ".tk[1]" -type "float3" -0.023361359 -0.033968441 0.00070647645 ;
	setAttr ".tk[2]" -type "float3" -0.02776099 -0.035707787 0.0080644144 ;
	setAttr ".tk[6]" -type "float3" -0.035859481 -0.0017455458 0.007135652 ;
	setAttr ".tk[7]" -type "float3" -0.052188106 -0.017266909 0.011191626 ;
	setAttr ".tk[10]" -type "float3" -0.047062196 -0.023851994 0.016207984 ;
	setAttr ".tk[11]" -type "float3" -0.038578089 -0.029035743 0.011340348 ;
	setAttr ".tk[44]" -type "float3" -0.025258532 -0.031397201 -0.0014708436 ;
	setAttr ".tk[45]" -type "float3" -0.031256482 -0.033489417 0.0080406731 ;
	setAttr ".tk[60]" -type "float3" -0.027508365 -0.033988729 0.0038923975 ;
	setAttr ".tk[84]" -type "float3" -0.052383337 -0.038456086 -0.00068504893 ;
	setAttr ".tk[85]" -type "float3" -0.0507068 -0.019963093 0.013208186 ;
	setAttr ".tk[86]" -type "float3" -0.031519763 -0.031759121 0.0050753569 ;
	setAttr ".tk[101]" -type "float3" -0.01893097 -0.035750434 0.0018458341 ;
	setAttr ".tk[102]" -type "float3" -0.025995618 -0.03286184 0.0014366268 ;
	setAttr ".tk[103]" -type "float3" -0.020938624 -0.035975847 0.0029598901 ;
	setAttr ".tk[104]" -type "float3" -0.025748249 -0.036881465 0.010564269 ;
	setAttr ".tk[122]" -type "float3" -0.030703468 -0.030226419 0.0010773405 ;
	setAttr ".tk[123]" -type "float3" -0.030852392 -0.024251061 0.013328361 ;
	setAttr ".tk[124]" -type "float3" -0.045023452 -0.025062105 0.013579545 ;
	setAttr ".tk[125]" -type "float3" -0.047887769 -0.02179007 0.01082876 ;
	setAttr ".tk[129]" -type "float3" -0.046626303 -0.024183573 0.018964147 ;
	setAttr ".tk[141]" -type "float3" -0.053063836 -0.016319631 0.015624602 ;
	setAttr ".tk[142]" -type "float3" -0.048757941 -0.0193255 0.0076922728 ;
	setAttr ".tk[144]" -type "float3" -0.022331152 -0.036486275 0.0048716129 ;
	setAttr ".tk[145]" -type "float3" -0.034146704 -0.023183791 -0.0019006209 ;
	setAttr ".tk[146]" -type "float3" -0.023413582 -0.03189224 -0.0012730249 ;
	setAttr ".tk[149]" -type "float3" -0.017842595 -0.010777918 -0.00026804331 ;
	setAttr ".tk[152]" -type "float3" -0.051977362 -0.018493455 0.016852755 ;
	setAttr ".tk[153]" -type "float3" -0.051418569 -0.015890475 0.010383744 ;
	setAttr ".tk[161]" -type "float3" -0.035384703 -0.030944955 0.009859493 ;
	setAttr ".tk[162]" -type "float3" -0.06060268 -0.033532102 -0.00049598748 ;
	setAttr ".tk[163]" -type "float3" -0.05802796 -0.029428098 0.014204421 ;
	setAttr ".tk[164]" -type "float3" -0.051753499 -0.0036253985 0.0065309769 ;
	setAttr ".tk[165]" -type "float3" -0.027708426 -0.02839601 -0.0014686362 ;
	setAttr ".tk[172]" -type "float3" -0.026987355 -0.042727455 0.0013970665 ;
	setAttr ".tk[173]" -type "float3" -0.043870486 -0.043753643 -0.0051593101 ;
	setAttr ".tk[174]" -type "float3" -0.042035952 -0.02686996 0.012157904 ;
	setAttr ".tk[189]" -type "float3" -0.016706798 -0.024289094 -0.0082692951 ;
	setAttr ".tk[190]" -type "float3" -0.053690165 -0.016797988 0.0087712444 ;
	setAttr ".tk[191]" -type "float3" -0.050224144 -0.018804936 0.0051730406 ;
	setAttr ".tk[192]" -type "float3" -0.051698931 -0.019339161 0.010734895 ;
	setAttr ".tk[193]" -type "float3" -0.049435459 -0.021145372 0.0080115069 ;
	setAttr ".tk[194]" -type "float3" -0.045084812 -0.022206163 0.0011473029 ;
	setAttr ".tk[195]" -type "float3" -0.045099124 -0.024635101 0.0055989712 ;
	setAttr ".tk[196]" -type "float3" -0.026208831 -0.0332537 -0.0011756942 ;
	setAttr ".tk[197]" -type "float3" -0.02798528 -0.033738382 0.0012526828 ;
	setAttr ".tk[198]" -type "float3" -0.031219419 -0.030392967 -0.0016927919 ;
	setAttr ".tk[199]" -type "float3" -0.032032441 -0.031581126 0.0024196706 ;
	setAttr ".tk[200]" -type "float3" -0.038519651 -0.026568806 -0.0016144897 ;
	setAttr ".tk[201]" -type "float3" -0.037653144 -0.029188585 0.0039388877 ;
	setAttr ".tk[202]" -type "float3" -0.024300158 -0.033006266 0.0027262212 ;
	setAttr ".tk[203]" -type "float3" -0.026196811 -0.03221003 0.0032488215 ;
	setAttr ".tk[204]" -type "float3" -0.026269576 -0.030833898 0.0011917484 ;
	setAttr ".tk[205]" -type "float3" -0.030190155 -0.029990932 0.0030241671 ;
	setAttr ".tk[206]" -type "float3" -0.029414456 -0.028426755 0.0011193478 ;
	setAttr ".tk[207]" -type "float3" -0.036138069 -0.026896972 0.0032393341 ;
	setAttr ".tk[208]" -type "float3" -0.050178759 -0.018238664 0.012470996 ;
	setAttr ".tk[209]" -type "float3" -0.050827496 -0.01754627 0.015649211 ;
	setAttr ".tk[210]" -type "float3" -0.04743503 -0.019801458 0.0096456073 ;
	setAttr ".tk[211]" -type "float3" -0.049343497 -0.017332399 0.011587257 ;
	setAttr ".tk[212]" -type "float3" -0.04341979 -0.022389811 0.0065597584 ;
	setAttr ".tk[213]" -type "float3" -0.046198223 -0.018279741 0.0083842399 ;
createNode polyAutoProj -n "polyAutoProj1";
	rename -uid "9FA0A24C-440E-5F2D-D785-44B2FB30FBCC";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:165]";
	setAttr ".ix" -type "matrix" -0.74860277734020875 1.0081788020545086 -0.4769062481574769 0
		 -0.49472423574038349 0.21459733533694267 1.2302300999904678 0 0.99955589989494753 0.86127444606199632 0.25172309435366008 0
		 0.011847419767537226 0.36194905690736434 0 1;
	setAttr ".s" -type "double3" 4.5442403322744012 4.5442403322744012 4.5442403322744012 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyTweak -n "polyTweak25";
	rename -uid "FED9BB30-4ECA-3604-E6A8-1CA7987E9AD3";
	setAttr ".uopa" yes;
	setAttr -s 159 ".tk[0:158]" -type "float3"  -0.0094329482 0.0053628916
		 -0.044081066 0.010116844 -0.0032776922 0.035185896 0.020889081 0.0060843895 0.0098399157
		 0.0078525022 0.00027564421 0.013529956 -0.040175643 -0.018400008 0.013809725 0.029645912
		 0.017738888 -0.030527959 0.0062277392 0.0084326472 -0.029413467 -0.0010156267 0.0021376435
		 -0.01237134 0.0094898334 -0.00018044039 0.018860981 -0.01688534 0.10521342 0.008878544
		 0.013336935 0.011977046 -0.033266928 0.04099071 0.016742028 -0.0041625937 -0.013543523
		 -0.011540515 0.030742116 0.015849791 0.0002479562 0.02881667 -0.012906637 -0.012197029
		 0.035157267 -0.031060211 -0.017735289 0.027830895 -0.024008727 -0.0087840958 -0.0025561964
		 0.010142547 -0.025385281 0.011895325 0.0072386507 0.0051217088 -0.011316891 0.015308699
		 0.0085485568 -0.012775459 0.007711201 0.0011145826 0.0091621596 0.028658394 0.0085920515
		 0.012303869 -0.0042057605 -0.00036971024 -0.0061612343 -0.038652323 -0.017964937
		 0.014569462 -0.027234651 -0.0046137092 -0.02904959 -0.026490323 -0.011126785 0.0041915318
		 -0.013935365 -0.0090010604 0.017588818 0.0065302714 0.011201811 -0.042373855 -0.020685297
		 -0.010247324 0.010891405 0.006755298 0.0018865165 0.0035785136 -0.015615631 -0.0026745885
		 -0.016513517 -0.010229371 -0.0036892467 -0.0013500089 -0.018817544 -0.0051778969
		 -0.010345491 -0.016574472 -0.0057126596 -0.0034823273 0.016181873 0.011852396 -0.027267797
		 -0.0013132277 0.046099648 -0.0036830823 0.035280231 0.012968176 0.0034622136 0.03166309
		 0.014508894 -0.010920534 -0.013868001 -0.010449747 0.024796514 -0.028077278 -0.015818875
		 0.024116285 -0.025964871 -0.013528421 0.016924392 -0.036147416 -0.015579515 0.0076570651
		 -0.021637531 -0.014969213 0.03216432 -0.041074105 -0.018786099 0.013994465 -0.034015656
		 -0.018498655 0.025962356 -0.036220968 -0.018899351 0.023742536 -0.023715103 -0.0027895181
		 -0.031296831 -0.030373305 -0.0066879303 -0.024858767 -0.025682207 -0.0035877547 -0.031122457
		 -0.032333408 -0.0079832869 -0.022241589 -0.029576372 -0.0077389553 -0.018212317 -0.061308205
		 -0.0076401304 -0.078813389 -0.009063934 0.0043833042 -0.038594469 0.0043170331 0.0095683318
		 -0.038583778 -0.0081926938 0.0043563074 -0.036811911 0.0042259591 0.0066457768 -0.024473118
		 -0.0093639353 0.0036288488 -0.035475641 -0.028289957 -0.0053111236 -0.027640508 0.0019755268
		 0.0075445175 -0.033129066 -0.039298877 -0.012588318 -0.012932288 -0.039138325 -0.01251801
		 -0.012971707 -0.018295664 -0.0060446644 -0.0051206639 -0.039897997 -0.01298276 -0.012139624
		 -0.037407592 -0.014978137 0.002330502 -0.0096464045 -0.0049424749 0.0058792792 -0.0035291947
		 -0.00479662 0.01675592 0.021554615 0.015045296 -0.032693245 0.016948288 0.014098475
		 -0.036792904 0.027128497 0.015094237 -0.02237235 0.025724951 0.011795515 -0.0089098234
		 0.020220507 0.012360241 -0.022098292 0.033837102 0.017920218 -0.023473676 0.026568824
		 0.0072111296 0.015093925 0.030363133 0.0099111907 0.0090866648 0.02076027 0.0035391622
		 0.022034999 0.0039796149 -0.0044038543 0.029062318 0.0037233727 -0.005675381 0.034791104
		 0.00017339934 -0.0078515289 0.038700785 -0.0054347124 -0.0098130945 0.037662484 0.011019867
		 -0.002941279 0.035252593 -0.012825091 -0.012504546 0.036814675 -0.0039300742 -0.0092395805
		 0.037710223 0.01272086 -0.0019400041 0.033581775 0.001739799 -0.0059012282 0.032136865
		 -0.026510958 -0.0142265 0.01930147 0.015660476 2.2003339e-006 0.029659066 0.013959969
		 0.0021533351 0.015924243 -0.017093694 -0.011140241 0.022059837 -0.016642211 -0.013643084
		 0.03514719 -0.028878059 -0.015932895 0.023156406 -0.026015177 -0.0048385393 -0.025640417
		 -0.00082870881 0.0022898461 -0.012761064 -0.0039920486 0.0030390201 -0.022415614
		 -0.014789196 -0.0012136173 -0.022087883 -0.0056946231 0.0047308323 -0.033909544 0.019948956
		 0.01558931 -0.038393993 0.018523887 0.014942707 -0.037933804 0.029207047 0.017560724
		 -0.030488696 0.034769658 0.01762663 -0.020272037 0.0050358381 0.0020407024 -0.00043265955
		 0.0021050533 0.00076136261 0.00026720611 0.014438623 -0.023022259 0.0084858593 0.021378841
		 0.0037812588 0.022023741 0.023888802 0.0079558669 0.0063767368 -0.0099807568 -0.0081806704
		 0.021071646 -0.011638533 -0.0054666754 0.0046669468 0.0029790858 0.0046420554 -0.017042743
		 0.016594451 0.010392632 -0.019351941 -0.017513551 -0.0049983473 -0.0087524913 -0.010569232
		 -0.001519623 -0.01259736 -0.0084672421 0.000160151 -0.016824456 -0.0074086129 -0.0059138476
		 0.014866251 -0.0083534718 -0.0037942682 0.0027172724 -0.014948401 -0.0052229604 -0.0027949046
		 -0.0130839 -0.0080898553 0.014748711 -0.0061976141 -0.0015444641 -0.0041936357 -0.014370147
		 -0.003677625 -0.0092517845 0.0019654168 0.0036368682 -0.014050618 0.012165197 0.0080110636
		 -0.016104173 0.011897013 0.0089599062 -0.021249486 -0.0041801026 0.0020537795 -0.017956786
		 -0.0016656531 0.0013448299 -0.0097281914 -0.0070945141 0.00011227101 -0.013989727
		 -0.0023782989 -0.040738754 0.015189424 -0.029452879 0.012752348 0.0010229556 -0.035622139
		 0.010339363 0.0011277264 -0.031758167 -0.02788844 0.0059816362 -0.022290656 -0.0063544759
		 -0.011175307 -0.031252936 -0.0087267235 -0.016561218 -0.016392417 -0.0010922356 -0.025718521
		 -0.0061860722 0.002783831 -0.025325168 -0.0061877663 0.0044985055 -0.033708394 -0.022047989
		 -0.0014514463 -0.034677833 0.010265497 0.010026566 -0.029553523 0.01335616 0.013092475
		 -0.038681872 0.020371329 0.0065612299 0.0065285671 0.01657892 0.0081694722 -0.0085162651
		 0.010302986 0.0025203235 0.0072022784 0.011787059 0.006870619 -0.011246957 0.0073032696
		 0.0031923852 -0.0017654084 0.018623693 0.0055093588 0.0083582904 0.0097854491 0.001867075
		 0.0094143562 0.014986136 0.0032514138 0.012501812 0.0075579165 0.00057773211 0.011495465
		 0.012620482 0.001997164 0.014149737 -0.033072367 -0.0084716454 -0.021254892 -0.03551086
		 -0.010083152 -0.017998952 -0.042834681 -0.014924807 -0.0082121212 -0.046730459 -0.017711908
		 -0.0019717056 -0.045655701 -0.017093372 -0.0029584467 -0.042909652 -0.015491483 -0.005584673
		 -0.033301804 -0.010045018 -0.014000113 -0.023952328 -0.0039742938 -0.025955968 -0.024362631
		 -0.0027156947 -0.032884393 -0.022968598 -0.0017944325 -0.034745738 -0.021999551 -0.0011540207
		 -0.03603965 -0.023211971 -0.0019552661 -0.034420796 -0.022442285 -0.0014466119 -0.035448477
		 -0.028652515 -0.0055507226 -0.027156413;
createNode polyAutoProj -n "polyAutoProj2";
	rename -uid "416FD32B-45B5-0986-0404-A7833343C1DA";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:115]";
	setAttr ".ix" -type "matrix" -0.84636494598209722 0.57245904792816504 -0.24732257576091207 0
		 0.54947926985451812 0.88176078290446236 0.16056741124825644 0 0.2948734474922739 -2.757431501596023e-014 -1.0090892377719289 0
		 0 1.6150360526818641 0 1;
	setAttr ".s" -type "double3" 4.5442403322744012 4.5442403322744012 4.5442403322744012 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyTweak -n "polyTweak26";
	rename -uid "5F445007-4826-4FC7-7D9B-91A99F9D8ACA";
	setAttr ".uopa" yes;
	setAttr -s 18 ".tk";
	setAttr ".tk[3]" -type "float3" 0.0019375551 -0.010134024 -0.0020874224 ;
	setAttr ".tk[20]" -type "float3" -0.04128556 -0.027784295 0.041067705 ;
	setAttr ".tk[21]" -type "float3" 0.09949401 0.017175227 -0.0018173129 ;
	setAttr ".tk[64]" -type "float3" 0.0019375551 -0.010134024 -0.0020874224 ;
	setAttr ".tk[108]" -type "float3" 0.09949401 0.017175227 -0.0018173129 ;
	setAttr ".tk[112]" -type "float3" 0.0019375551 -0.010134024 -0.0020874224 ;
	setAttr ".tk[113]" -type "float3" 0.0019375551 -0.010134024 -0.0020874224 ;
	setAttr ".tk[116]" -type "float3" -0.028308012 -0.02307108 0.039176743 ;
	setAttr ".tk[117]" -type "float3" -0.0089625251 0.015748361 0.024619993 ;
	setAttr ".tk[118]" -type "float3" -0.10053559 -0.065337926 0.025468793 ;
	setAttr ".tk[119]" -type "float3" -0.077520221 -0.033765908 0.030555079 ;
	setAttr ".tk[120]" -type "float3" 0.0075109042 -0.048328109 0.028927987 ;
	setAttr ".tk[121]" -type "float3" 0.027625864 -0.006431167 0.015372533 ;
	setAttr ".tk[122]" -type "float3" -0.032904759 -0.075084202 0.033771176 ;
	setAttr ".tk[123]" -type "float3" -0.0094619198 -0.031896312 0.019996086 ;
	setAttr ".tk[124]" -type "float3" 0.00020807543 -0.069764607 -0.049610894 ;
	setAttr ".tk[125]" -type "float3" -0.01608349 -0.11041127 -0.032698166 ;
createNode polyAutoProj -n "polyAutoProj3";
	rename -uid "05DF7F81-4123-E1B3-791D-ACBCDF49CC87";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:217]";
	setAttr ".ix" -type "matrix" -0.92810740213313181 0.5945596759751397 -0.30597306776889177 0
		 0.56466562455059632 0.97724248172816242 0.18615568953579709 0 0.3581528429964711 -1.1159972072809353e-014 -1.0863841942164962 0
		 0 2.7817231000642724 0 1;
	setAttr ".s" -type "double3" 4.5442403322744012 4.5442403322744012 4.5442403322744012 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyTweak -n "polyTweak27";
	rename -uid "455F6DD2-4BAB-7341-A727-198F96AB0BE4";
	setAttr ".uopa" yes;
	setAttr -s 4 ".tk[214:217]" -type "float3"  -0.057204485 -0.0073278039
		 0.026923528 -0.0031672004 -0.069580473 -0.0048834053 -0.055265926 0.031652458 0.011332985
		 0.045593072 -0.035122957 -0.030927336;
createNode polyAutoProj -n "polyAutoProj4";
	rename -uid "16C0F709-4123-76E3-0AF7-F2B32D037C30";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:150]";
	setAttr ".ix" -type "matrix" -0.81447242258580377 0.52906747753934713 -0.23816439082567392 0
		 0.50780243874826869 0.84857975712961264 0.14848932281870789 0 0.28066235238899212 5.4539706084710831e-015 -0.95980656590350399 0
		 0 3.8727167106890019 0 1;
	setAttr ".s" -type "double3" 4.5442403322744012 4.5442403322744012 4.5442403322744012 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyTweak -n "polyTweak28";
	rename -uid "D996D221-4545-4A2F-C757-C88F6A2D496C";
	setAttr ".uopa" yes;
	setAttr -s 13 ".tk";
	setAttr ".tk[111]" -type "float3" 0.0060271826 0.009737825 -0.028427949 ;
	setAttr ".tk[112]" -type "float3" 0.0060271826 0.009737825 -0.028427949 ;
	setAttr ".tk[113]" -type "float3" 0.0060271826 0.009737825 -0.028427949 ;
	setAttr ".tk[114]" -type "float3" 0.0060271826 0.009737825 -0.028427949 ;
	setAttr ".tk[115]" -type "float3" 0.0060271826 0.009737825 -0.028427949 ;
	setAttr ".tk[116]" -type "float3" 0.0060271826 0.009737825 -0.028427949 ;
	setAttr ".tk[117]" -type "float3" 0.0060271826 0.009737825 -0.028427949 ;
	setAttr ".tk[118]" -type "float3" 0.0060271751 0.009737825 -0.028427949 ;
	setAttr ".tk[119]" -type "float3" 0.0060271826 0.009737825 -0.028427949 ;
	setAttr ".tk[120]" -type "float3" 0.0060271826 0.009737825 -0.028427949 ;
	setAttr ".tk[121]" -type "float3" 0.0060271826 0.009737825 -0.028427949 ;
	setAttr ".tk[122]" -type "float3" 0.0060271826 0.009737825 -0.028427949 ;
createNode polyAutoProj -n "polyAutoProj5";
	rename -uid "24A01420-4901-2004-B544-068CC31EB59E";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:165]";
	setAttr ".ix" -type "matrix" -0.74860277734020875 1.0081788020545086 -0.4769062481574769 0
		 -0.49472423574038349 0.21459733533694267 1.2302300999904678 0 0.99955589989494753 0.86127444606199632 0.25172309435366008 0
		 0.011847419767537226 0.36194905690736434 0 1;
	setAttr ".s" -type "double3" 1.5834792209719852 1.5834792209719852 1.5834792209719852 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj6";
	rename -uid "A29A703D-4EB1-7E60-32C5-FD93521EB5C9";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:165]";
	setAttr ".ix" -type "matrix" -0.74860277734020875 1.0081788020545086 -0.4769062481574769 0
		 -0.49472423574038349 0.21459733533694267 1.2302300999904678 0 0.99955589989494753 0.86127444606199632 0.25172309435366008 0
		 0.011847419767537226 0.36194905690736434 0 1;
	setAttr ".s" -type "double3" 1.5834792209719852 1.5834792209719852 1.5834792209719852 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj7";
	rename -uid "380D77BD-42CD-37F8-69B8-E5B6CB77C054";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:165]";
	setAttr ".ix" -type "matrix" -0.74860277734020875 1.0081788020545086 -0.4769062481574769 0
		 -0.49472423574038349 0.21459733533694267 1.2302300999904678 0 0.99955589989494753 0.86127444606199632 0.25172309435366008 0
		 0.011847419767537226 0.36194905690736434 0 1;
	setAttr ".s" -type "double3" 1.5834792209719852 1.5834792209719852 1.5834792209719852 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj8";
	rename -uid "91314FC7-416C-60B4-FE0D-ADA015D25AC6";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:217]";
	setAttr ".ix" -type "matrix" -0.92810740213313181 0.5945596759751397 -0.30597306776889177 0
		 0.56466562455059632 0.97724248172816242 0.18615568953579709 0 0.3581528429964711 -1.1159972072809353e-014 -1.0863841942164962 0
		 0 2.7817231000642724 0 1;
	setAttr ".s" -type "double3" 1.6433188411159436 1.6433188411159436 1.6433188411159436 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode transformGeometry -n "transformGeometry1";
	rename -uid "CAB15EAD-492E-B533-D189-D9A28C7779BD";
	setAttr ".txf" -type "matrix" -0.74860277734020875 1.0081788020545086 -0.4769062481574769 0
		 -0.49472423574038349 0.21459733533694267 1.2302300999904678 0 0.99955589989494753 0.86127444606199632 0.25172309435366008 0
		 0 0 0 1;
createNode transformGeometry -n "transformGeometry2";
	rename -uid "2667D752-471B-E279-9568-7294844FFA90";
	setAttr ".txf" -type "matrix" -0.84636494598209722 0.57245904792816504 -0.24732257576091207 0
		 0.54947926985451812 0.88176078290446236 0.16056741124825644 0 0.2948734474922739 -2.757431501596023e-014 -1.0090892377719289 0
		 0 0 0 1;
createNode transformGeometry -n "transformGeometry3";
	rename -uid "72F38374-4A25-228E-8533-C2800586F9C5";
	setAttr ".txf" -type "matrix" -0.92810740213313181 0.5945596759751397 -0.30597306776889177 0
		 0.56466562455059632 0.97724248172816242 0.18615568953579709 0 0.3581528429964711 -1.1159972072809353e-014 -1.0863841942164962 0
		 0 0 0 1;
createNode transformGeometry -n "transformGeometry4";
	rename -uid "A4C33CF0-4272-5EB6-514F-50922F35CF2C";
	setAttr ".txf" -type "matrix" -0.81447242258580377 0.52906747753934713 -0.23816439082567392 0
		 0.50780243874826869 0.84857975712961264 0.14848932281870789 0 0.28066235238899212 5.4539706084710831e-015 -0.95980656590350399 0
		 0 0 0 1;
createNode polyAutoProj -n "polyAutoProj9";
	rename -uid "E80FC50F-4F8A-7E69-09BE-70B85B6B1AD8";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:150]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.5510798096656799 1.5510798096656799 1.5510798096656799 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj10";
	rename -uid "D82A6CED-4970-CE27-E13A-78911C869F79";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:150]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.5510798096656799 1.5510798096656799 1.5510798096656799 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj11";
	rename -uid "0EF2FE37-4F2C-A731-A7A4-53BA2BDE1AAF";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:217]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.643318772315979 1.643318772315979 1.643318772315979 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj12";
	rename -uid "E2643304-491E-A683-28EA-63AB2694DF0F";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:115]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.5867202877998352 1.5867202877998352 1.5867202877998352 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj13";
	rename -uid "DEDB9879-4885-89B4-05E0-C5B79FEDBF31";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:165]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.5834791660308838 1.5834791660308838 1.5834791660308838 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyTweak -n "polyTweak29";
	rename -uid "09137D98-42E6-2872-F8F4-12BC455327E9";
	setAttr ".uopa" yes;
	setAttr -s 147 ".tk[0:146]" -type "float3"  0 0.0042084148 0 0 0.0081383232
		 0 0 -0.0079524107 0 0 0.013190024 0 0 -0.013039619 0 0 0.0079524107 0 0 -0.0040691555
		 0 0 0.016572254 0 0 -0.028045863 0 0 0.027054168 0 0 -0.028045863 0 0 -0.02118244
		 0 0 0.027054183 0 0 0.027054174 0 0 0.027054172 0 0 0.022911433 0 0 -0.00010110869
		 0 0 0.00010111024 0 0 0.027054172 0 0 -0.0095106792 0 0 -0.028045863 0 0 -0.022911433
		 0 0 -0.028045863 0 0 -0.028045863 0 0 -0.028045863 0 0 0.021182433 0 0 0.013330156
		 0 0 -0.0039256522 0 0 0.021317899 0 0 0.017468764 0 0 0.0041197143 0 0 0.027054172
		 0 0 0.021267332 0 0 0.027054183 0 0 0.0098116351 0 0 0.013330152 0 0 0.021317899
		 0 0 -0.0040393858 0 0 -0.028045863 0 0 -0.015431933 0 0 0.013266345 0 0 -0.013266345
		 0 0 -0.013190022 0 0 0.0041503618 0 0 -0.028045863 0 0 -0.028045863 0 0 0.0043299231
		 0 0 -0.028045872 0 0 -0.028045863 0 0 -0.028045863 0 0 -0.028045859 0 0 -0.013086786
		 0 0 -0.021100651 0 0 -0.0095943818 0 0 -0.013190024 0 0 -0.0041503594 0 0 0.0040691621
		 0 0 0.01130655 0 0 0.0041503608 0 0 0.027054163 0 0 0.027054183 0 0 0.027054172 0
		 0 0.027054172 0 0 0.027054163 0 0 0.027054181 0 0 0.027054174 0 0 0.027054181 0 0
		 0.027054181 0 0 0.021100657 0 0 0.013086781 0 0 0.0039256522 0 0 0.009594379 0 0
		 0.015431916 0 0 -0.0041197143 0 0 -0.021317899 0 0 -0.0042084088 0 0 -0.013330158
		 0 0 0.0040393872 0 0 0.0095106792 0 0 0.013039608 0 0 -0.013330159 0 0 -0.0098116351
		 0 0 -0.021317899 0 0 -0.0043299231 0 0 -0.013266345 0 0 -0.021267351 0 0 0.013266333
		 0 0 0.023192495 0 0 0.027054174 0 0 0.020160256 0 0 0.015813751 0 0 0.015602263 0
		 0 0.0086306427 0 0 0.018187005 0 0 0.027054179 0 0 0.011203297 0 0 0.022947866 0
		 0 0.027054172 0 0 0.018238606 0 0 0.02004689 0 0 -0.0045579891 0 0 0.0043784273 0
		 0 0.027054181 0 0 0.016161783 0 0 0.0076890225 0 0 1.2869059e-009 0 0 -0.0086701959
		 0 0 -0.015388422 0 0 -8.9783367e-005 0 0 -0.0087981308 0 0 -0.015539501 0 0 0.013595154
		 0 0 0.010859465 0 0 0.020578858 0 0 0.018042574 0 0 0.01912714 0 0 0.025502771 0
		 0 0.01988872 0 0 0.025612459 0 0 0.013611034 0 0 0.020543117 0 0 0.0098780328 0 0
		 0.018350791 0 0 -0.0054682689 0 0 -0.004889661 0 0 -0.011605461 0 0 -0.011658875
		 0 0 -0.0083227633 0 0 -0.013367644 0 0 -0.0086314939 0 0 -0.013261942 0 0 0.0083498564
		 0 0 0.0043548448 0 0 -0.00015047671 0 0 -0.0046453821 0 0 0.008067905 0 0 0.015376736
		 0 0 0.0044022435 0 0 0.010071525 0 0 0.00073659036 0 0 0.0067596585 0 0 0.0083590979
		 0 0 0.014799984 0 0 0.010366884 0 0 0.0050207437 0 0 0.0081242342 0 0 0.0012932395
		 0;
createNode transformGeometry -n "transformGeometry5";
	rename -uid "E638E068-4545-0150-BD75-1B97AA3420BE";
	setAttr ".txf" -type "matrix" 1.0763710782340126 0 0 0 0 1.0763710782340126 0 0
		 0 0 1.0763710782340126 0 3.2078357882321255e-008 0.010342479599912843 -0.054791599572688233 1;
createNode transformGeometry -n "transformGeometry6";
	rename -uid "B285BCFE-4D7E-9D67-EC11-37BF5E0EA59A";
	setAttr ".txf" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.045259535312652588 -0.01197284460067749 -0.054400712251663208 1;
createNode transformGeometry -n "transformGeometry7";
	rename -uid "96278F60-4880-D40E-4A2F-DD929CFB37D0";
	setAttr ".txf" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0.019917041063308716 -0.017875611782073975 -0.023837119340896606 1;
createNode polyTweak -n "polyTweak30";
	rename -uid "439B356F-42DC-9F83-A726-F1BF77195C3B";
	setAttr ".uopa" yes;
	setAttr -s 159 ".tk[0:158]" -type "float3"  0 -0.032057349 0 0 0.027542543
		 0 0 0.021341862 0 0 0.013583342 0 0 -0.022530988 0 0 0.0051223449 0 0 -0.011933515
		 0 0 -0.007764549 0 0 0.017835258 0 0 0.0091358544 0 0 -0.0087440005 0 0 0.028603192
		 0 0 0.007159886 0 0 0.028269695 0 0 0.010138199 0 0 -0.0077159945 0 0 -0.019578094
		 0 0 0.010396052 0 0 -0.00093420141 0 0 0.0043355841 0 0 0.0110061 0 0 0.028603192
		 0 0 -0.0066612745 0 0 -0.020950794 0 0 -0.036999695 0 0 -0.017635614 0 0 -0.00057585165
		 0 0 -0.019035719 0 0 -0.0094618518 0 0 0.0071259299 0 0 -0.021133903 0 0 -0.0084891822
		 0 0 -0.020063324 0 0 -0.014487314 0 0 -0.0032021594 0 0 0.0037345954 0 0 0.028603194
		 0 0 0.017736204 0 0 0.0035518594 0 0 -0.0075642439 0 0 -0.010036778 0 0 -0.022968814
		 0 0 0.001851434 0 0 -0.023105042 0 0 -0.011004936 0 0 -0.013926052 0 0 -0.035612691
		 0 0 -0.036999691 0 0 -0.036999695 0 0 -0.036999695 0 0 -0.032638438 0 0 -0.03579203
		 0 0 -0.028675314 0 0 -0.018563487 0 0 -0.02700907 0 0 -0.010650955 0 0 -0.027137809
		 0 0 -0.036999688 0 0 -0.017246567 0 0 -0.036994714 0 0 -0.036895756 0 0 -0.016713906
		 0 0 -0.036998842 0 0 -0.026933368 0 0 -0.003959856 0 0 0.0068121534 0 0 -0.0022132276
		 0 0 -0.0080109462 0 0 0.007834103 0 0 0.014388803 0 0 0.0027719536 0 0 0.012277743
		 0 0 0.028603194 0 0 0.028070951 0 0 0.028142394 0 0 0.019443855 0 0 0.022490662 0
		 0 0.022021005 0 0 0.017198266 0 0 0.028262269 0 0 0.011137248 0 0 0.018361628 0 0
		 0.028601866 0 0 0.019491298 0 0 -0.0091046719 0 0 0.028603194 0 0 0.01955018 0 0
		 -0.00043222457 0 0 0.0073112571 0 0 -0.0087119546 0 0 -0.034150396 0 0 -0.0078438167
		 0 0 -0.015693715 0 0 -0.023662737 0 0 -0.023480786 0 0 -0.0066503487 0 0 -0.0074663199
		 0 0 0.0048131049 0 0 0.014792968 0 0 0.003558524 0 0 0.0017409495 0 0 0.01171213
		 0 0 0.028603192 0 0 0.021648504 0 0 0.0043807793 0 0 -0.006150106 0 0 -0.0073898491
		 0 0 0.0015868235 0 0 -0.018177466 0 0 -0.015107615 0 0 -0.015911058 0 0 0.002813441
		 0 0 -0.0047718883 0 0 -0.012870427 0 0 -0.001539221 0 0 -0.0070526754 0 0 -0.016085869
		 0 0 -0.0064629945 0 0 7.870327e-005 0 0 -0.0030341442 0 0 -0.013313727 0 0 -0.0067604459
		 0 0 -0.013270937 0 0 -0.0074680764 0 0 -0.018044868 0 0 -0.022644848 0 0 -0.013921043
		 0 0 -0.023155704 0 0 -0.032970738 0 0 -0.026927121 0 0 -0.018996431 0 0 -0.023739457
		 0 0 -0.036265999 0 0 -0.0089632738 0 0 -0.01179229 0 0 0.019077862 0 0 0.0077040019
		 0 0 0.011854954 0 0 0.0025404752 0 0 0.0045171347 0 0 0.01879292 0 0 0.012715295
		 0 0 0.018389376 0 0 0.012210108 0 0 0.017534852 0 0 -0.036999691 0 0 -0.036999688
		 0 0 -0.036995243 0 0 -0.036407754 0 0 -0.036154184 0 0 -0.035565723 0 0 -0.033069491
		 0 0 -0.032770939 0 0 -0.036999691 0 0 -0.036999691 0 0 -0.036999691 0 0 -0.036999695
		 0 0 -0.036999688 0 0 -0.036999688 0;
createNode transformGeometry -n "transformGeometry8";
	rename -uid "C429F6C0-4E2C-695F-66F1-119882BDEBF7";
	setAttr ".txf" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -0.0059453248977661133 -0.074512094259262085 -0.038339853286743164 1;
createNode polyAutoProj -n "polyAutoProj14";
	rename -uid "9AD0A6B3-44DB-F1E6-7A8F-FBB16A9F297D";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:150]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.6695374846458435 1.6695374846458435 1.6695374846458435 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj15";
	rename -uid "FACA9F9E-4A90-0245-B587-AA94EB74C1F3";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:217]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.643318772315979 1.643318772315979 1.643318772315979 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj16";
	rename -uid "197C2E88-4406-7297-E337-519D93A1C7ED";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:115]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.5867202281951904 1.5867202281951904 1.5867202281951904 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj17";
	rename -uid "A8FDCE82-4458-6786-8966-A7AE006F529C";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:165]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.5834791660308838 1.5834791660308838 1.5834791660308838 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj18";
	rename -uid "7E32C920-433B-7011-AAF2-E489F838E30C";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:150]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.6695374846458435 1.6695374846458435 1.6695374846458435 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj19";
	rename -uid "6979416E-4A50-0DEA-FFFC-93AA189B50BA";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:217]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.643318772315979 1.643318772315979 1.643318772315979 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj20";
	rename -uid "65BCF8A0-4507-9694-26FE-20BB6DDF08D2";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:115]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.5867202281951904 1.5867202281951904 1.5867202281951904 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
createNode polyAutoProj -n "polyAutoProj21";
	rename -uid "B8FF825C-45D3-D8AE-25F0-04BC5038B4C0";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[0:165]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1;
	setAttr ".s" -type "double3" 1.5834791660308838 1.5834791660308838 1.5834791660308838 ;
	setAttr ".ps" 0.20000000298023224;
	setAttr ".dl" yes;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
	setAttr ".fprt" yes;
select -ne :renderPartition;
	setAttr -s 2 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 4 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :initialShadingGroup;
	setAttr -s 4 ".dsm";
	setAttr ".ro" yes;
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultRenderGlobals;
	setAttr ".ren" -type "string" "arnold";
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :ikSystem;
	setAttr -s 4 ".sol";
connectAttr "polyAutoProj18.out" "angryShape.i";
connectAttr "polyAutoProj19.out" "derpShape.i";
connectAttr "polyAutoProj20.out" "reaperShape.i";
connectAttr "polyAutoProj21.out" "dreamworksShape.i";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "polyPlatonicSolid1.out" "polySmoothFace1.ip";
connectAttr "polySmoothFace1.out" "polySubdFace1.ip";
connectAttr "polySubdFace1.out" "polyReduce1.ip";
connectAttr "polyReduce1.out" "polyReduce2.ip";
connectAttr "pasted__polyPlatonicSolid1.out" "pasted__polySmoothFace1.ip";
connectAttr "pasted__polySmoothFace1.out" "pasted__polySubdFace1.ip";
connectAttr "polyTweak1.out" "polySubdFace2.ip";
connectAttr "polyReduce2.out" "polyTweak1.ip";
connectAttr "polySubdFace2.out" "polyReduce3.ip";
connectAttr "polyReduce3.out" "polySplit1.ip";
connectAttr "polySplit1.out" "polySplit2.ip";
connectAttr "polySplit2.out" "polySplit3.ip";
connectAttr "polySplit3.out" "polySplit4.ip";
connectAttr "polyTweak2.out" "polyExtrudeFace1.ip";
connectAttr "angryShape.wm" "polyExtrudeFace1.mp";
connectAttr "polySplit4.out" "polyTweak2.ip";
connectAttr "polyExtrudeFace1.out" "deleteComponent1.ig";
connectAttr "deleteComponent1.og" "polyAppend1.ip";
connectAttr "polyAppend1.out" "polyAppend2.ip";
connectAttr "polyAppend2.out" "polyAppend3.ip";
connectAttr "polyAppend3.out" "polyAppend4.ip";
connectAttr "polyAppend4.out" "polyAppend5.ip";
connectAttr "polyAppend5.out" "polyAppend6.ip";
connectAttr "polyAppend6.out" "polySplit5.ip";
connectAttr "polySplit5.out" "polySplit6.ip";
connectAttr "polySplit6.out" "polySplit7.ip";
connectAttr "polySplit7.out" "polySplit8.ip";
connectAttr "polySplit8.out" "polyExtrudeFace2.ip";
connectAttr "angryShape.wm" "polyExtrudeFace2.mp";
connectAttr "polyExtrudeFace2.out" "polyTweak3.ip";
connectAttr "polyTweak3.out" "polySplit9.ip";
connectAttr "polySplit9.out" "polyTweak4.ip";
connectAttr "polyTweak4.out" "deleteComponent2.ig";
connectAttr "polyTweak5.out" "polyExtrudeFace3.ip";
connectAttr "angryShape.wm" "polyExtrudeFace3.mp";
connectAttr "deleteComponent2.og" "polyTweak5.ip";
connectAttr "pasted__pasted__polySmoothFace1.out" "pasted__pasted__polySubdFace1.ip"
		;
connectAttr "pasted__pasted__polyPlatonicSolid1.out" "pasted__pasted__polySmoothFace1.ip"
		;
connectAttr "pasted__polySubdFace1.out" "polyReduce4.ip";
connectAttr "polyReduce4.out" "polySmoothFace2.ip";
connectAttr "polySmoothFace2.out" "polyReduce5.ip";
connectAttr "polyReduce5.out" "polyReduce6.ip";
connectAttr "polyReduce6.out" "polySmoothFace3.ip";
connectAttr "polySmoothFace3.out" "polyReduce7.ip";
connectAttr "polyReduce7.out" "polyReduce8.ip";
connectAttr "polyReduce8.out" "polyTweak6.ip";
connectAttr "polyTweak6.out" "polySplit10.ip";
connectAttr "polySplit10.out" "polySplit11.ip";
connectAttr "polyTweak7.out" "polyExtrudeFace4.ip";
connectAttr "derpShape.wm" "polyExtrudeFace4.mp";
connectAttr "polySplit11.out" "polyTweak7.ip";
connectAttr "polyExtrudeFace4.out" "polyTweak8.ip";
connectAttr "polyTweak8.out" "deleteComponent3.ig";
connectAttr "deleteComponent3.og" "polyAppend7.ip";
connectAttr "polyAppend7.out" "polyAppend8.ip";
connectAttr "polyAppend8.out" "polyAppend9.ip";
connectAttr "polyAppend9.out" "polyAppend10.ip";
connectAttr "polyAppend10.out" "polyAppend11.ip";
connectAttr "polyAppend11.out" "polyAppend12.ip";
connectAttr "polyTweak9.out" "polyExtrudeFace5.ip";
connectAttr "derpShape.wm" "polyExtrudeFace5.mp";
connectAttr "polyAppend12.out" "polyTweak9.ip";
connectAttr "pasted__pasted__pasted__polySmoothFace1.out" "pasted__pasted__pasted__polySubdFace1.ip"
		;
connectAttr "pasted__pasted__pasted__polyPlatonicSolid1.out" "pasted__pasted__pasted__polySmoothFace1.ip"
		;
connectAttr "pasted__pasted__polySubdFace1.out" "polyReduce9.ip";
connectAttr "polyReduce9.out" "polyTweak10.ip";
connectAttr "polyTweak10.out" "deleteComponent4.ig";
connectAttr "deleteComponent4.og" "polySplit12.ip";
connectAttr "polySplit12.out" "polySubdFace3.ip";
connectAttr "polySubdFace3.out" "polyReduce10.ip";
connectAttr "polyReduce10.out" "polySmoothFace4.ip";
connectAttr "polySmoothFace4.out" "createColorSet1.ig";
connectAttr "createColorSet1.og" "createColorSet2.ig";
connectAttr "createColorSet2.og" "polyTweak11.ip";
connectAttr "polyTweak11.out" "polySplit13.ip";
connectAttr "polySplit13.out" "polyTweak12.ip";
connectAttr "polyTweak12.out" "deleteComponent5.ig";
connectAttr "deleteComponent5.og" "deleteComponent6.ig";
connectAttr "deleteComponent6.og" "polySplit14.ip";
connectAttr "polySplit14.out" "polySplit15.ip";
connectAttr "polySplit15.out" "deleteComponent7.ig";
connectAttr "polyTweak13.out" "polyExtrudeFace6.ip";
connectAttr "reaperShape.wm" "polyExtrudeFace6.mp";
connectAttr "deleteComponent7.og" "polyTweak13.ip";
connectAttr "polyExtrudeFace6.out" "polyExtrudeFace7.ip";
connectAttr "reaperShape.wm" "polyExtrudeFace7.mp";
connectAttr "polyExtrudeFace7.out" "polyTweak14.ip";
connectAttr "polyTweak14.out" "deleteComponent8.ig";
connectAttr "deleteComponent8.og" "deleteComponent9.ig";
connectAttr "deleteComponent9.og" "polySplit16.ip";
connectAttr "polySplit16.out" "polySplit17.ip";
connectAttr "polyTweak15.out" "polySplit18.ip";
connectAttr "polySplit17.out" "polyTweak15.ip";
connectAttr "polySplit18.out" "deleteComponent10.ig";
connectAttr "deleteComponent10.og" "deleteComponent11.ig";
connectAttr "deleteComponent11.og" "polyCloseBorder1.ip";
connectAttr "polyCloseBorder1.out" "polyCloseBorder2.ip";
connectAttr "polyTweak16.out" "polyExtrudeFace8.ip";
connectAttr "reaperShape.wm" "polyExtrudeFace8.mp";
connectAttr "polyCloseBorder2.out" "polyTweak16.ip";
connectAttr "pasted__pasted__pasted__polySubdFace1.out" "polyReduce11.ip";
connectAttr "polyReduce11.out" "polySmoothFace5.ip";
connectAttr "polySmoothFace5.out" "polyReduce12.ip";
connectAttr "polyReduce12.out" "polyReduce13.ip";
connectAttr "polyReduce13.out" "polySubdFace4.ip";
connectAttr "polySubdFace4.out" "polyReduce14.ip";
connectAttr "polyReduce14.out" "polyTweak17.ip";
connectAttr "polyTweak17.out" "polySplit19.ip";
connectAttr "polySplit19.out" "polySplit20.ip";
connectAttr "polySplit20.out" "polySplit21.ip";
connectAttr "polySplit21.out" "polySplit22.ip";
connectAttr "polyTweak18.out" "polyExtrudeFace9.ip";
connectAttr "dreamworksShape.wm" "polyExtrudeFace9.mp";
connectAttr "polySplit22.out" "polyTweak18.ip";
connectAttr "polyExtrudeFace9.out" "polyExtrudeFace10.ip";
connectAttr "dreamworksShape.wm" "polyExtrudeFace10.mp";
connectAttr "polyTweak19.out" "polyExtrudeFace11.ip";
connectAttr "dreamworksShape.wm" "polyExtrudeFace11.mp";
connectAttr "polyExtrudeFace10.out" "polyTweak19.ip";
connectAttr "polyTweak20.out" "polySplit23.ip";
connectAttr "polyExtrudeFace11.out" "polyTweak20.ip";
connectAttr "polySplit23.out" "polySplit24.ip";
connectAttr "polySplit24.out" "createColorSet3.ig";
connectAttr "createColorSet3.og" "createColorSet4.ig";
connectAttr "polyTweak21.out" "polySplit25.ip";
connectAttr "createColorSet4.og" "polyTweak21.ip";
connectAttr "polySplit25.out" "deleteComponent12.ig";
connectAttr "deleteComponent12.og" "polySplit26.ip";
connectAttr "polySplit26.out" "deleteComponent13.ig";
connectAttr "deleteComponent13.og" "polySplit27.ip";
connectAttr "polySplit27.out" "polySplit28.ip";
connectAttr "polySplit28.out" "deleteComponent14.ig";
connectAttr "deleteComponent14.og" "polySplit29.ip";
connectAttr "polySplit29.out" "polySplit30.ip";
connectAttr "polySplit30.out" "deleteComponent15.ig";
connectAttr "deleteComponent15.og" "polySplit31.ip";
connectAttr "polySplit31.out" "polyTweak22.ip";
connectAttr "polyTweak22.out" "deleteComponent16.ig";
connectAttr "deleteComponent16.og" "polySplit32.ip";
connectAttr "polySplit32.out" "deleteComponent17.ig";
connectAttr "deleteComponent17.og" "polySplit33.ip";
connectAttr "polyTweak23.out" "polySplit34.ip";
connectAttr "polySplit33.out" "polyTweak23.ip";
connectAttr "polySplit34.out" "polySplit35.ip";
connectAttr "polyTweak24.out" "polyExtrudeFace12.ip";
connectAttr "derpShape.wm" "polyExtrudeFace12.mp";
connectAttr "polyExtrudeFace5.out" "polyTweak24.ip";
connectAttr "polyTweak25.out" "polyAutoProj1.ip";
connectAttr "dreamworksShape.wm" "polyAutoProj1.mp";
connectAttr "polySplit35.out" "polyTweak25.ip";
connectAttr "polyTweak26.out" "polyAutoProj2.ip";
connectAttr "reaperShape.wm" "polyAutoProj2.mp";
connectAttr "polyExtrudeFace8.out" "polyTweak26.ip";
connectAttr "polyTweak27.out" "polyAutoProj3.ip";
connectAttr "derpShape.wm" "polyAutoProj3.mp";
connectAttr "polyExtrudeFace12.out" "polyTweak27.ip";
connectAttr "polyTweak28.out" "polyAutoProj4.ip";
connectAttr "angryShape.wm" "polyAutoProj4.mp";
connectAttr "polyExtrudeFace3.out" "polyTweak28.ip";
connectAttr "polyAutoProj1.out" "polyAutoProj5.ip";
connectAttr "dreamworksShape.wm" "polyAutoProj5.mp";
connectAttr "polyAutoProj5.out" "polyAutoProj6.ip";
connectAttr "dreamworksShape.wm" "polyAutoProj6.mp";
connectAttr "polyAutoProj6.out" "polyAutoProj7.ip";
connectAttr "dreamworksShape.wm" "polyAutoProj7.mp";
connectAttr "polyAutoProj3.out" "polyAutoProj8.ip";
connectAttr "derpShape.wm" "polyAutoProj8.mp";
connectAttr "polyAutoProj7.out" "transformGeometry1.ig";
connectAttr "polyAutoProj2.out" "transformGeometry2.ig";
connectAttr "polyAutoProj8.out" "transformGeometry3.ig";
connectAttr "polyAutoProj4.out" "transformGeometry4.ig";
connectAttr "transformGeometry4.og" "polyAutoProj9.ip";
connectAttr "angryShape.wm" "polyAutoProj9.mp";
connectAttr "polyAutoProj9.out" "polyAutoProj10.ip";
connectAttr "angryShape.wm" "polyAutoProj10.mp";
connectAttr "transformGeometry3.og" "polyAutoProj11.ip";
connectAttr "derpShape.wm" "polyAutoProj11.mp";
connectAttr "transformGeometry2.og" "polyAutoProj12.ip";
connectAttr "reaperShape.wm" "polyAutoProj12.mp";
connectAttr "transformGeometry1.og" "polyAutoProj13.ip";
connectAttr "dreamworksShape.wm" "polyAutoProj13.mp";
connectAttr "polyAutoProj10.out" "polyTweak29.ip";
connectAttr "polyTweak29.out" "transformGeometry5.ig";
connectAttr "polyAutoProj11.out" "transformGeometry6.ig";
connectAttr "polyAutoProj12.out" "transformGeometry7.ig";
connectAttr "polyAutoProj13.out" "polyTweak30.ip";
connectAttr "polyTweak30.out" "transformGeometry8.ig";
connectAttr "transformGeometry5.og" "polyAutoProj14.ip";
connectAttr "angryShape.wm" "polyAutoProj14.mp";
connectAttr "transformGeometry6.og" "polyAutoProj15.ip";
connectAttr "derpShape.wm" "polyAutoProj15.mp";
connectAttr "transformGeometry7.og" "polyAutoProj16.ip";
connectAttr "reaperShape.wm" "polyAutoProj16.mp";
connectAttr "transformGeometry8.og" "polyAutoProj17.ip";
connectAttr "dreamworksShape.wm" "polyAutoProj17.mp";
connectAttr "polyAutoProj14.out" "polyAutoProj18.ip";
connectAttr "angryShape.wm" "polyAutoProj18.mp";
connectAttr "polyAutoProj15.out" "polyAutoProj19.ip";
connectAttr "derpShape.wm" "polyAutoProj19.mp";
connectAttr "polyAutoProj16.out" "polyAutoProj20.ip";
connectAttr "reaperShape.wm" "polyAutoProj20.mp";
connectAttr "polyAutoProj17.out" "polyAutoProj21.ip";
connectAttr "dreamworksShape.wm" "polyAutoProj21.mp";
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
connectAttr "angryShape.iog" ":initialShadingGroup.dsm" -na;
connectAttr "derpShape.iog" ":initialShadingGroup.dsm" -na;
connectAttr "reaperShape.iog" ":initialShadingGroup.dsm" -na;
connectAttr "dreamworksShape.iog" ":initialShadingGroup.dsm" -na;
// End of rocks.ma
