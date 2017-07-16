using System;
using System.Collections.Generic;
using System.Linq;
using SynDataFileGen.Lib;

namespace CCLF17.Lib
{
	public static class CCLFData
	{
		#region Categorical Distribution Lists

		// //////////////////////////////////////////////////
		// Generic Lists
		public static readonly List<Category> LIST_YN = new List<Category>() { new Category() { Value = "Y" }, new Category() { Value = "N" } };

		private static string[] ARR_ALPHACAPS_NUMERICS = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
		public static readonly List<Category> LIST_ALPHACAPS_NUMERICS = ARR_ALPHACAPS_NUMERICS.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.census.gov/geo/reference/ansi_statetables.html
		private static string[] ARR_FIPS_STATE_CD = { "01", "02", "04", "05", "06", "08", "09", "10", "11", "12", "13", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "44", "45", "46", "47", "48", "49", "50", "51", "53", "54", "55", "56", "60", "64", "66", "67", "68", "69", "70", "71", "72", "74", "76", "78", "79", "81", "84", "86", "89", "95" };
		public static readonly List<Category> LIST_FIPS_STATE_CD = ARR_FIPS_STATE_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.resdac.org/cms-data/variables/patient-discharge-status-code
		private static string[] ARR_STUS_CD = { "0", "01", "02", "04", "05", "06", "08", "09", "20", "21", "30", "40", "41", "42", "43", "50", "51", "61", "62", "63", "64", "65", "66", "69", "70", "71", "72", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95" };
		public static readonly List<Category> LIST_STUS_CD = ARR_STUS_CD.Select(a => new Category() { Value = a }).ToList();
		// //////////////////////////////////////////////////



		// //////////////////////////////////////////////////
		// Field-Specific

		// TODO replace with correct values from authoritative source
		private static int[] ARR_BENE_FIPS_CNTY_CD = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279, 280, 281, 282, 283, 284, 285, 286, 287, 288, 289, 290, 291, 292, 293, 294, 295, 296, 297, 298, 299, 300, 301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 321, 322, 323, 324, 325, 326, 327, 328, 329, 330, 331, 332, 333, 334, 335, 336, 337, 338, 339, 340, 341, 342, 343, 344, 345, 346, 347, 348, 349, 350, 351, 352, 353, 354, 355, 356, 357, 358, 359, 360, 361, 362, 363, 364, 365, 366, 367, 368, 369, 370, 371, 372, 373, 374, 375, 376, 377, 378, 379, 380, 381, 382, 383, 384, 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396, 397, 398, 399, 400, 401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 420, 421, 422, 423, 424, 425, 426, 427, 428, 429, 430, 431, 432, 433, 434, 435, 436, 437, 438, 439, 440, 441, 442, 443, 444, 445, 446, 447, 448, 449, 450, 451, 452, 453, 454, 455, 456, 457, 458, 459, 460, 461, 462, 463, 464, 465, 466, 467, 468, 469, 470, 471, 472, 473, 474, 475, 476, 477, 478, 479, 480, 481, 482, 483, 484, 485, 486, 487, 488, 489, 490, 491, 492, 493, 494, 495, 496, 497, 498, 499, 500, 501, 502, 503, 504, 505, 506, 507, 508, 509, 510, 511, 512, 513, 514, 515, 516, 517, 518, 519, 520, 521, 522, 523, 524, 525, 526, 527, 528, 529, 530, 531, 532, 533, 534, 535, 536, 537, 538, 539, 540, 541, 542, 543, 544, 545, 546, 547, 548, 549, 550, 551, 552, 553, 554, 555, 556, 557, 558, 559, 560, 561, 562, 563, 564, 565, 566, 567, 568, 569, 570, 571, 572, 573, 574, 575, 576, 577, 578, 579, 580, 581, 582, 583, 584, 585, 586, 587, 588, 589, 590, 591, 592, 593, 594, 595, 596, 597, 598, 599, 600, 601, 602, 603, 604, 605, 606, 607, 608, 609, 610, 611, 612, 613, 614, 615, 616, 617, 618, 619, 620, 621, 622, 623, 624, 625, 626, 627, 628, 629, 630, 631, 632, 633, 634, 635, 636, 637, 638, 639, 640, 641, 642, 643, 644, 645, 646, 647, 648, 649, 650, 651, 652, 653, 654, 655, 656, 657, 658, 659, 660, 661, 662, 663, 664, 665, 666, 667, 668, 669, 670, 671, 672, 673, 674, 675, 676, 677, 678, 679, 680, 681, 682, 683, 684, 685, 686, 687, 688, 689, 690, 691, 692, 693, 694, 695, 696, 697, 698, 699, 700, 701, 702, 703, 704, 705, 706, 707, 708, 709, 710, 711, 712, 713, 714, 715, 716, 717, 718, 719, 720, 721, 722, 723, 724, 725, 726, 727, 728, 729, 730, 731, 732, 733, 734, 735, 736, 737, 738, 739, 740, 741, 742, 743, 744, 745, 746, 747, 748, 749, 750, 751, 752, 753, 754, 755, 756, 757, 758, 759, 760, 761, 762, 763, 764, 765, 766, 767, 768, 769, 770, 771, 772, 773, 774, 775, 776, 777, 778, 779, 780, 781, 782, 783, 784, 785, 786, 787, 788, 789, 790, 791, 792, 793, 794, 795, 796, 797, 798, 799, 800, 801, 802, 803, 804, 805, 806, 807, 808, 809, 810, 811, 812, 813, 814, 815, 816, 817, 818, 819, 820, 821, 822, 823, 824, 825, 826, 827, 828, 829, 830, 831, 832, 833, 834, 835, 836, 837, 838, 839, 840, 841, 842, 843, 844, 845, 846, 847, 848, 849, 850, 851, 852, 853, 854, 855, 856, 857, 858, 859, 860, 861, 862, 863, 864, 865, 866, 867, 868, 869, 870, 871, 872, 873, 874, 875, 876, 877, 878, 879, 880, 881, 882, 883, 884, 885, 886, 887, 888, 889, 890, 891, 892, 893, 894, 895, 896, 897, 898, 899, 900, 901, 902, 903, 904, 905, 906, 907, 908, 909, 910, 911, 912, 913, 914, 915, 916, 917, 918, 919, 920, 921, 922, 923, 924, 925, 926, 927, 928, 929, 930, 931, 932, 933, 934, 935, 936, 937, 938, 939, 940, 941, 942, 943, 944, 945, 946, 947, 948, 949, 950, 951, 952, 953, 954, 955, 956, 957, 958, 959, 960, 961, 962, 963, 964, 965, 966, 967, 968, 969, 970, 971, 972, 973, 974, 975, 976, 977, 978, 979, 980, 981, 982, 983, 984, 985, 986, 987, 988, 989, 990, 991, 992, 993, 994, 995, 996, 997, 998, 999 };
		public static readonly List<Category> LIST_BENE_FIPS_CNTY_CD = ARR_BENE_FIPS_CNTY_CD.Select(a => new Category() { Value = a }).ToList();

		// TODO replace with correct values from authoritative source? Or is this OK for US zip codes?
		private static string[] ARR_BENE_ZIP_CD = Enumerable.Range(1, 99999).Select(a => a.ToString().PadLeft(5, '0')).ToArray();
		public static readonly List<Category> LIST_BENE_ZIP_CD = ARR_BENE_ZIP_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.findacode.com/code-set.php?set=HCPCS
		public static readonly List<Category> LIST_HCPCS_CD = GetHCPCSCodes();

		// Source: https://www.findacode.com/search/search.php, search for HCPCS, use the CPT Modifier codes
		private static string[] ARR_HCPCS_CPT_MOD_CD = { "22", "23", "24", "25", "26", "27", "32", "33", "47", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "62", "63", "66", "73", "74", "76", "77", "78", "79", "80", "81", "82", "90", "91", "92", "99" };
		public static readonly List<Category> LIST_HCPCS_CPT_MOD_CD = ARR_HCPCS_CPT_MOD_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static int[] ARR_BENE_SEX_CD = { 0, 1, 2 };
		public static readonly List<Category> LIST_BENE_SEX_CD = ARR_BENE_SEX_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static int[] ARR_BENE_RACE_CD = { 0, 1, 2, 3, 4, 5, 6 };
		public static readonly List<Category> LIST_BENE_RACE_CD = ARR_BENE_RACE_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static int[] ARR_MDCR_STUS_CD = { 10, 11, 20, 21, 31, 5, 6 };
		public static readonly List<Category> LIST_BENE_MDCR_STUS_CD = ARR_MDCR_STUS_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.resdac.org/cms-data/variables/Dual-Status-Code-occurs-12-times
		private static string[] ARR_BENE_DUAL_STUS_CD = { "**", "NA", "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "99" };
		public static readonly List<Category> LIST_BENE_DUAL_STUS_CD = ARR_BENE_DUAL_STUS_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.ssa.gov/OACT/babynames/decades/names2000s.html + cleanup and de-dupe
		private static string[] ARR_BENE_1ST_NAME = { "Aaliyah", "Aaron", "Abby", "Abigail", "Abraham", "Adam", "Addison", "Adrian", "Adriana", "Adrianna", "Aidan", "Aiden", "Alan", "Alana", "Alejandro", "Alex", "Alexa", "Alexander", "Alexandra", "Alexandria", "Alexia", "Alexis", "Alicia", "Allison", "Alondra", "Alyssa", "Amanda", "Amber", "Amelia", "Amy", "Ana", "Andrea", "Andres", "Andrew", "Angel", "Angela", "Angelica", "Angelina", "Anna", "Anthony", "Antonio", "Ariana", "Arianna", "Ashley", "Ashlyn", "Ashton", "Aubrey", "Audrey", "Austin", "Autumn", "Ava", "Avery", "Ayden", "Bailey", "Benjamin", "Bianca", "Blake", "Braden", "Bradley", "Brady", "Brandon", "Brayden", "Breanna", "Brendan", "Brian", "Briana", "Brianna", "Brittany", "Brody", "Brooke", "Brooklyn", "Bryan", "Bryce", "Bryson", "Caden", "Caitlin", "Caitlyn", "Caleb", "Cameron", "Camila", "Carlos", "Caroline", "Carson", "Carter", "Cassandra", "Cassidy", "Catherine", "Cesar", "Charles", "Charlotte", "Chase", "Chelsea", "Cheyenne", "Chloe", "Christian", "Christina", "Christopher", "Claire", "Cody", "Colby", "Cole", "Colin", "Collin", "Colton", "Conner", "Connor", "Cooper", "Courtney", "Cristian", "Crystal", "Daisy", "Dakota", "Dalton", "Damian", "Daniel", "Daniela", "Danielle", "David", "Delaney", "Derek", "Destiny", "Devin", "Devon", "Diana", "Diego", "Dominic", "Donovan", "Dylan", "Edgar", "Eduardo", "Edward", "Edwin", "Eli", "Elias", "Elijah", "Elizabeth", "Ella", "Ellie", "Emily", "Emma", "Emmanuel", "Eric", "Erica", "Erick", "Erik", "Erin", "Ethan", "Eva", "Evan", "Evelyn", "Faith", "Fernando", "Francisco", "Gabriel", "Gabriela", "Gabriella", "Gabrielle", "Gage", "Garrett", "Gavin", "Genesis", "George", "Gianna", "Giovanni", "Giselle", "Grace", "Gracie", "Grant", "Gregory", "Hailey", "Haley", "Hannah", "Hayden", "Hector", "Henry", "Hope", "Hunter", "Ian", "Isaac", "Isabel", "Isabella", "Isabelle", "Isaiah", "Ivan", "Jack", "Jackson", "Jacob", "Jacqueline", "Jada", "Jade", "Jaden", "Jake", "Jalen", "James", "Jared", "Jasmin", "Jasmine", "Jason", "Javier", "Jayden", "Jayla", "Jazmin", "Jeffrey", "Jenna", "Jennifer", "Jeremiah", "Jeremy", "Jesse", "Jessica", "Jesus", "Jillian", "Jocelyn", "Joel", "John", "Johnathan", "Jonah", "Jonathan", "Jordan", "Jordyn", "Jorge", "Jose", "Joseph", "Joshua", "Josiah", "Juan", "Julia", "Julian", "Juliana", "Justin", "Kaden", "Kaitlyn", "Kaleb", "Karen", "Karina", "Kate", "Katelyn", "Katherine", "Kathryn", "Katie", "Kayla", "Kaylee", "Kelly", "Kelsey", "Kendall", "Kennedy", "Kenneth", "Kevin", "Kiara", "Kimberly", "Kyle", "Kylee", "Kylie", "Landon", "Laura", "Lauren", "Layla", "Leah", "Leonardo", "Leslie", "Levi", "Liam", "Liliana", "Lillian", "Lilly", "Lily", "Lindsey", "Logan", "Lucas", "Lucy", "Luis", "Luke", "Lydia", "Mackenzie", "Madeline", "Madelyn", "Madison", "Makayla", "Makenzie", "Malachi", "Manuel", "Marco", "Marcus", "Margaret", "Maria", "Mariah", "Mario", "Marissa", "Mark", "Martin", "Mary", "Mason", "Matthew", "Max", "Maxwell", "Maya", "Mckenzie", "Megan", "Melanie", "Melissa", "Mia", "Micah", "Michael", "Michelle", "Miguel", "Mikayla", "Miranda", "Molly", "Morgan", "Mya", "Naomi", "Natalia", "Natalie", "Nathan", "Nathaniel", "Nevaeh", "Nicholas", "Nicolas", "Nicole", "Noah", "Nolan", "Oliver", "Olivia", "Omar", "Oscar", "Owen", "Paige", "Parker", "Patrick", "Paul", "Payton", "Peter", "Peyton", "Preston", "Rachel", "Raymond", "Reagan", "Rebecca", "Ricardo", "Richard", "Riley", "Robert", "Ruby", "Ryan", "Rylee", "Sabrina", "Sadie", "Samantha", "Samuel", "Sara", "Sarah", "Savannah", "Sean", "Sebastian", "Serenity", "Sergio", "Seth", "Shane", "Shawn", "Shelby", "Sierra", "Skylar", "Sofia", "Sophia", "Sophie", "Spencer", "Stephanie", "Stephen", "Steven", "Summer", "Sydney", "Tanner", "Taylor", "Thomas", "Tiffany", "Timothy", "Travis", "Trenton", "Trevor", "Trinity", "Tristan", "Tyler", "Valeria", "Valerie", "Vanessa", "Veronica", "Victor", "Victoria", "Vincent", "Wesley", "William", "Wyatt", "Xavier", "Zachary", "Zoe", "Zoey" };
		public static readonly List<Category> LIST_BENE_1ST_NAME = ARR_BENE_1ST_NAME.Select(a => new Category() { Value = a }).ToList();

		// Source: me
		private static string[] ARR_BENE_MIDL_NAME = Enumerable.Range(65, 26).Select(a => ((char)a).ToString()).ToArray();
		public static readonly List<Category> LIST_BENE_MIDL_NAME = ARR_BENE_MIDL_NAME.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www2.census.gov/topics/genealogy/2010surnames/Names_2010Census_Top1000.xlsx + cleanup
		private static string[] ARR_BENE_LAST_NAME = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper", "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson", "Watson", "Brooks", "Chavez", "Wood", "James", "Bennett", "Gray", "Mendoza", "Ruiz", "Hughes", "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers", "Long", "Ross", "Foster", "Jimenez", "Powell", "Jenkins", "Perry", "Russell", "Sullivan", "Bell", "Coleman", "Butler", "Henderson", "Barnes", "Gonzales", "Fisher", "Vasquez", "Simmons", "Romero", "Jordan", "Patterson", "Alexander", "Hamilton", "Graham", "Reynolds", "Griffin", "Wallace", "Moreno", "West", "Cole", "Hayes", "Bryant", "Herrera", "Gibson", "Ellis", "Tran", "Medina", "Aguilar", "Stevens", "Murray", "Ford", "Castro", "Marshall", "Owens", "Harrison", "Fernandez", "Mcdonald", "Woods", "Washington", "Kennedy", "Wells", "Vargas", "Henry", "Chen", "Freeman", "Webb", "Tucker", "Guzman", "Burns", "Crawford", "Olson", "Simpson", "Porter", "Hunter", "Gordon", "Mendez", "Silva", "Shaw", "Snyder", "Mason", "Dixon", "Munoz", "Hunt", "Hicks", "Holmes", "Palmer", "Wagner", "Black", "Robertson", "Boyd", "Rose", "Stone", "Salazar", "Fox", "Warren", "Mills", "Meyer", "Rice", "Schmidt", "Garza", "Daniels", "Ferguson", "Nichols", "Stephens", "Soto", "Weaver", "Ryan", "Gardner", "Payne", "Grant", "Dunn", "Kelley", "Spencer", "Hawkins", "Arnold" };
		public static readonly List<Category> LIST_BENE_LAST_NAME = ARR_BENE_LAST_NAME.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static int[] ARR_BENE_ORGNL_ENTLMT_RSN_CD = { 0, 1, 2, 3, 4 };
		public static readonly List<Category> LIST_BENE_ORGNL_ENTLMT_RSN_CD = ARR_BENE_ORGNL_ENTLMT_RSN_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static string[] ARR_BENE_ENTLMT_BUYIN_IND = { "0", "1", "2", "3", "A", "B", "C" };
		public static readonly List<Category> LIST_BENE_ENTLMT_BUYIN_IND = ARR_BENE_ENTLMT_BUYIN_IND.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static int[] ARR_CLM_TYPE_CD = { 10, 20, 30, 40, 50, 60, 61 };
		public static readonly List<Category> LIST_CLM_TYPE_CD = ARR_CLM_TYPE_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static int[] ARR_CLM_BILL_FAC_TYPE_CD = Enumerable.Range(1, 9).ToArray();
		public static readonly List<Category> LIST_CLM_BILL_FAC_TYPE_CD = ARR_CLM_BILL_FAC_TYPE_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: http://www.resdac.org/cms-data/variables/Claim-Service-classification-Type-Code
		private static string[] ARR_CLM_BILL_CLSFCTN_CD = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
		public static readonly List<Category> LIST_CLM_BILL_CLSFCTN_CD = ARR_CLM_BILL_CLSFCTN_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.resdac.org/cms-data/variables/claim-medicare-non-payment-reason-code
		private static string[] ARR_CLM_MDCR_NPMT_RSN_CD = { "A", "B", "C", "E", "F", "G", "H", "I", "J", "K", "N", "P", "Q", "R", "T", "U", "V", "W", "X", "Y", "Z", "00", "12", "13", "14", "15", "16", "17", "18", "19", "21", "22", "25", "26", "42", "43" };
		public static readonly List<Category> LIST_CLM_MDCR_NPMT_RSN_CD = ARR_CLM_MDCR_NPMT_RSN_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.resdac.org/cms-data/variables/NCH-Primary-Payer-Code
		private static string[] ARR_CLM_NCH_PRMRY_PYR_CD = { string.Empty, "A", "B", "C", "E", "F", "G", "H", "I", "J", "L", "M", "N", "Y", "Z" };
		public static readonly List<Category> LIST_CLM_NCH_PRMRY_PYR_CD = ARR_CLM_NCH_PRMRY_PYR_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.hipaaspace.com/Medical.Coding.Library/DRGs/
		private static string[] ARR_DGNS_DRG_CD = { "001", "002", "003", "004", "005", "006", "007", "008", "010", "011", "012", "013", "014", "016", "017", "020", "021", "022", "023", "024", "025", "026", "027", "028", "029", "030", "031", "032", "033", "034", "035", "036", "037", "038", "039", "040", "041", "042", "052", "053", "054", "055", "056", "057", "058", "059", "060", "061", "062", "063", "064", "065", "066", "067", "068", "069", "070", "071", "072", "073", "074", "075", "076", "077", "078", "079", "080", "081", "082", "083", "084", "085", "086", "087", "088", "089", "090", "091", "092", "093", "094", "095", "096", "097", "098", "099", "100", "101", "102", "103", "113", "114", "115", "116", "117", "121", "122", "123", "124", "125", "129", "130", "131", "132", "133", "134", "135", "136", "137", "138", "139", "146", "147", "148", "149", "150", "151", "152", "153", "154", "155", "156", "157", "158", "159", "163", "164", "165", "166", "167", "168", "175", "176", "177", "178", "179", "180", "181", "182", "183", "184", "185", "186", "187", "188", "189", "190", "191", "192", "193", "194", "195", "196", "197", "198", "199", "200", "201", "202", "203", "204", "205", "206", "207", "208", "215", "216", "217", "218", "219", "220", "221", "222", "223", "224", "225", "226", "227", "228", "229", "231", "232", "233", "234", "235", "236", "239", "240", "241", "242", "243", "244", "245", "246", "247", "248", "249", "250", "251", "252", "253", "254", "255", "256", "257", "258", "259", "260", "261", "262", "263", "264", "265", "266", "267", "268", "269", "270", "271", "272", "273", "274", "280", "281", "282", "283", "284", "285", "286", "287", "288", "289", "290", "291", "292", "293", "294", "295", "296", "297", "298", "299", "300", "301", "302", "303", "304", "305", "306", "307", "308", "309", "310", "311", "312", "313", "314", "315", "316", "326", "327", "328", "329", "330", "331", "332", "333", "334", "335", "336", "337", "338", "339", "340", "341", "342", "343", "344", "345", "346", "347", "348", "349", "350", "351", "352", "353", "354", "355", "356", "357", "358", "368", "369", "370", "371", "372", "373", "374", "375", "376", "377", "378", "379", "380", "381", "382", "383", "384", "385", "386", "387", "388", "389", "390", "391", "392", "393", "394", "395", "405", "406", "407", "408", "409", "410", "411", "412", "413", "414", "415", "416", "417", "418", "419", "420", "421", "422", "423", "424", "425", "432", "433", "434", "435", "436", "437", "438", "439", "440", "441", "442", "443", "444", "445", "446", "453", "454", "455", "456", "457", "458", "459", "460", "461", "462", "463", "464", "465", "466", "467", "468", "469", "470", "471", "472", "473", "474", "475", "476", "477", "478", "479", "480", "481", "482", "483", "485", "486", "487", "488", "489", "492", "493", "494", "495", "496", "497", "498", "499", "500", "501", "502", "503", "504", "505", "506", "507", "508", "509", "510", "511", "512", "513", "514", "515", "516", "517", "518", "519", "520", "533", "534", "535", "536", "537", "538", "539", "540", "541", "542", "543", "544", "545", "546", "547", "548", "549", "550", "551", "552", "553", "554", "555", "556", "557", "558", "559", "560", "561", "562", "563", "564", "565", "566", "570", "571", "572", "573", "574", "575", "576", "577", "578", "579", "580", "581", "582", "583", "584", "585", "592", "593", "594", "595", "596", "597", "598", "599", "600", "601", "602", "603", "604", "605", "606", "607", "614", "615", "616", "617", "618", "619", "620", "621", "622", "623", "624", "625", "626", "627", "628", "629", "630", "637", "638", "639", "640", "641", "642", "643", "644", "645", "652", "653", "654", "655", "656", "657", "658", "659", "660", "661", "662", "663", "664", "665", "666", "667", "668", "669", "670", "671", "672", "673", "674", "675", "682", "683", "684", "685", "686", "687", "688", "689", "690", "691", "692", "693", "694", "695", "696", "697", "698", "699", "700", "707", "708", "709", "710", "711", "712", "713", "714", "715", "716", "717", "718", "722", "723", "724", "725", "726", "727", "728", "729", "730", "734", "735", "736", "737", "738", "739", "740", "741", "742", "743", "744", "745", "746", "747", "748", "749", "750", "754", "755", "756", "757", "758", "759", "760", "761", "765", "766", "767", "768", "769", "770", "774", "775", "776", "777", "778", "779", "780", "781", "782", "789", "790", "791", "792", "793", "794", "795", "799", "800", "801", "802", "803", "804", "808", "809", "810", "811", "812", "813", "814", "815", "816", "820", "821", "822", "823", "824", "825", "826", "827", "828", "829", "830", "834", "835", "836", "837", "838", "839", "840", "841", "842", "843", "844", "845", "846", "847", "848", "849", "853", "854", "855", "856", "857", "858", "862", "863", "864", "865", "866", "867", "868", "869", "870", "871", "872", "876", "880", "881", "882", "883", "884", "885", "886", "887", "894", "895", "896", "897", "901", "902", "903", "904", "905", "906", "907", "908", "909", "913", "914", "915", "916", "917", "918", "919", "920", "921", "922", "923", "927", "928", "929", "933", "934", "935", "939", "940", "941", "945", "946", "947", "948", "949", "950", "951", "955", "956", "957", "958", "959", "963", "964", "965", "969", "970", "974", "975", "976", "977", "981", "982", "983", "984", "985", "986", "987", "988", "989", "998", "999" };
		public static readonly List<Category> LIST_DGNS_DRG_CD = ARR_DGNS_DRG_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet for values, weights here are contrived
		public static readonly List<Category> LIST_CLM_OP_SRVC_TYPE_CD = new List<Category>()
		{
			new Category() {Value = "0", Weight = 5},
			new Category() {Value = "1", Weight = 5},
			new Category() {Value = "2", Weight = 5},
			new Category() {Value = "3", Weight = 5},
			new Category() {Value = "5", Weight = 0.1},
			new Category() {Value = "6", Weight = 0.1},
			new Category() {Value = "7", Weight = 0.1},
			new Category() {Value = "8", Weight = 0.1},
			new Category() {Value = "9", Weight = 1}
		};

		// Source: CCLF Info packet
		private static string[] ARR_CLM_ADJSMT_TYPE_CD = { "0", "1", "2" };
		public static readonly List<Category> LIST_CLM_ADJSMT_TYPE_CD = ARR_CLM_ADJSMT_TYPE_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet for values, weights here are contrived
		public static readonly List<Category> LIST_CLM_ADMSN_TYPE_CD = new List<Category>()
		{
			new Category() {Value = "0", Weight = 5},
			new Category() {Value = "1", Weight = 5},
			new Category() {Value = "2", Weight = 5},
			new Category() {Value = "3", Weight = 5},
			new Category() {Value = "4", Weight = 1},
			new Category() {Value = "5", Weight = 3},
			new Category() {Value = "6", Weight = 0.1},
			new Category() {Value = "7", Weight = 0.1},
			new Category() {Value = "8", Weight = 0.1},
			new Category() {Value = "9", Weight = 1}
		};

		// Source: https://www.resdac.org/cms-data/variables/Claim-Source-Inpatient-Admission-Code
		private static string[] ARR_CLM_ADMSN_SRC_CD = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "E", "F" };
		public static readonly List<Category> LIST_CLM_ADMSN_SRC_CD = ARR_CLM_ADMSN_SRC_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: http://www.resdac.org/cms-data/variables/Claim-Frequency-Code
		private static string[] ARR_CLM_BILL_FREQ_CD = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "E", "F", "G", "H", "I", "J", "K", "M", "P", "X", "Z" };
		public static readonly List<Category> LIST_CLM_BILL_FREQ_CD = ARR_CLM_BILL_FREQ_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static string[] ARR_CLM_QUERY_CD = { "0", "1", "2", "3", "4", "5" };
		public static readonly List<Category> LIST_CLM_QUERY_CD = ARR_CLM_QUERY_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static string[] ARR_DGNS_PRCDR_ICD_IND = { "0", "9", "U" };
		public static readonly List<Category> LIST_DGNS_PRCDR_ICD_IND = ARR_DGNS_PRCDR_ICD_IND.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.resdac.org/cms-data/variables/revenue-center-code
		private static string[] ARR_CLM_LINE_PROD_REV_CTR_CD = { "0022", "0023", "0024", "0100", "0101", "0110", "0111", "0112", "0113", "0114", "0115", "0116", "0117", "0118", "0119", "0120", "0121", "0122", "0123", "0124", "0125", "0126", "0127", "0128", "0129", "0130", "0131", "0132", "0133", "0134", "0135", "0136", "0137", "0138", "0139", "0140", "0141", "0142", "0143", "0144", "0145", "0146", "0147", "0148", "0149", "0150", "0151", "0152", "0153", "0154", "0155", "0156", "0157", "0158", "0159", "0160", "0164", "0167", "0169", "0170", "0171", "0172", "0173", "0174", "0175", "0179", "0180", "0182", "0183", "0184", "0185", "0189", "0190", "0191", "0192", "0193", "0194", "0199", "0200", "0201", "0202", "0203", "0204", "0206", "0207", "0208", "0209", "0210", "0211", "0212", "0213", "0214", "0219", "0220", "0221", "0222", "0223", "0224", "0229", "0230", "0231", "0232", "0233", "0234", "0235", "0239", "0240", "0241", "0242", "0243", "0249", "0250", "0251", "0252", "0253", "0254", "0255", "0256", "0257", "0258", "0259", "0260", "0261", "0262", "0263", "0264", "0269", "0270", "0271", "0272", "0273", "0274", "0275", "0276", "0277", "0278", "0279", "0280", "0289", "0290", "0291", "0292", "0293", "0294", "0299", "0300", "0301", "0302", "0303", "0304", "0305", "0306", "0307", "0309", "0310", "0311", "0312", "0314", "0319", "0320", "0321", "0322", "0323", "0324", "0329", "0330", "0331", "0332", "0333", "0335", "0339", "0340", "0341", "0342", "0349", "0350", "0351", "0352", "0359", "0360", "0361", "0362", "0367", "0369", "0370", "0371", "0372", "0374", "0379", "0380", "0381", "0382", "0383", "0384", "0385", "0386", "0387", "0389", "0390", "0391", "0399", "0400", "0401", "0402", "0403", "0404", "0409", "0410", "0412", "0413", "0419", "0420", "0421", "0422", "0423", "0424", "0429", "0430", "0431", "0432", "0433", "0434", "0439", "0440", "0441", "0442", "0443", "0444", "0449", "0450", "0451", "0452", "0456", "0459", "0460", "0469", "0470", "0471", "0472", "0479", "0480", "0481", "0482", "0483", "0489", "0490", "0499", "0500", "0509", "0510", "0511", "0512", "0513", "0514", "0515", "0516", "0517", "0519", "0520", "0521", "0522", "0523", "0524", "0525", "0526", "0527", "0528", "0529", "0530", "0531", "0539", "0540", "0541", "0542", "0543", "0544", "0545", "0546", "0547", "0548", "0549", "0550", "0551", "0552", "0559", "0560", "0561", "0562", "0569", "0570", "0571", "0572", "0579", "0580", "0581", "0582", "0589", "0590", "0599", "0600", "0601", "0602", "0603", "0604", "0610", "0611", "0612", "0614", "0615", "0616", "0618", "0619", "0621", "0622", "0623", "0624", "0630", "0631", "0632", "0633", "0634", "0635", "0636", "0637", "0640", "0641", "0642", "0643", "0644", "0645", "0646", "0647", "0648", "0649", "0650", "0651", "0652", "0655", "0656", "0657", "0659", "0660", "0661", "0662", "0670", "0671", "0672", "0679", "0700", "0709", "0710", "0719", "0720", "0721", "0722", "0723", "0724", "0729", "0730", "0731", "0732", "0739", "0740", "0749", "0750", "0759", "0760", "0761", "0762", "0769", "0770", "0771", "0779", "0780", "0789", "0790", "0799", "0800", "0801", "0802", "0803", "0804", "0809", "0810", "0811", "0812", "0813", "0814", "0815", "0816", "0817", "0819", "0820", "0821", "0822", "0823", "0824", "0825", "0829", "0830", "0831", "0832", "0833", "0834", "0835", "0839", "0840", "0841", "0842", "0843", "0844", "0845", "0849", "0850", "0851", "0852", "0853", "0854", "0855", "0859", "0880", "0881", "0882", "0889", "0890", "0891", "0892", "0893", "0899", "0900", "0901", "0902", "0903", "0904", "0905", "0906", "0907", "0909", "0910", "0911", "0912", "0913", "0914", "0915", "0916", "0917", "0918", "0919", "0920", "0921", "0922", "0923", "0924", "0925", "0929", "0931", "0932", "0940", "0941", "0942", "0943", "0944", "0945", "0946", "0947", "0949", "0951", "0952", "0960", "0961", "0962", "0963", "0964", "0969", "0971", "0972", "0973", "0974", "0975", "0976", "0977", "0978", "0979", "0981", "0982", "0983", "0984", "0985", "0986", "0987", "0988", "0989", "0990", "0991", "0992", "0993", "0994", "0995", "0996", "0997", "0998", "0999", "9000", "9001", "9002", "9003", "9004", "9005", "9006", "9007", "9008", "9009", "9010", "9011", "9012", "9013", "9014", "9015", "9016", "9017", "9018", "9019", "9020", "9021", "9022", "9023", "9024", "9025", "9026", "9027", "9028", "9029", "9030", "9031", "9032", "9033", "9034", "9035", "9036", "9037", "9038", "9039", "9040", "9041", "9042", "9043", "9044" };
		public static readonly List<Category> LIST_CLM_LINE_PROD_REV_CTR_CD = ARR_CLM_LINE_PROD_REV_CTR_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static string[] ARR_CLM_PROD_TYPE_CD = { "E", "1", "D" };
		public static readonly List<Category> LIST_CLM_PROD_TYPE_CD = ARR_CLM_PROD_TYPE_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.resdac.org/cms-data/variables/claim-diagnosis-code-i-diagnosis-present-admission-indicator-code
		private static string[] ARR_CLM_POA_IND = { "Y", "N", "U", "W", "1", "Z", "X", "0", string.Empty };
		public static readonly List<Category> LIST_CLM_POA_IND = ARR_CLM_POA_IND.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static int[] ARR_CCLF5_CLM_TYPE_CD = { 71, 72 };
		public static readonly List<Category> LIST_CCLF5_CLM_TYPE_CD = ARR_CCLF5_CLM_TYPE_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static string[] ARR_RNDRG_PRVDR_TYPE_CD = { "0", "1", "2", "3", "4", "5", "6", "7", "UI", "N2", "D", "BP", "BG", "A" };
		public static readonly List<Category> LIST_RNDRG_PRVDR_TYPE_CD = ARR_RNDRG_PRVDR_TYPE_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.resdac.org/cms-data/variables/Line-HCFA-Provider-Specialty-Code
		private static string[] ARR_CLM_PRVDR_SPCLTY_CD = { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "B1", "B2", "B3", "B4" };
		public static readonly List<Category> LIST_CLM_PRVDR_SPCLTY_CD = ARR_CLM_PRVDR_SPCLTY_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.resdac.org/cms-data/variables/Line-HCFA-Type-Service-Code
		private static string[] ARR_CLM_FED_TYPE_SRVC_CD = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "Y", "Z" };
		public static readonly List<Category> LIST_CLM_FED_TYPE_SRVC_CD = ARR_CLM_FED_TYPE_SRVC_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.resdac.org/cms-data/variables/line-place-service-code
		private static string[] ARR_CLM_POS_CD = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99" };
		public static readonly List<Category> LIST_CLM_POS_CD = ARR_CLM_POS_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.resdac.org/cms-data/variables/Line-Beneficiary-Primary-Payer-Code
		private static string[] ARR_PRMRY_PYR_CD = { string.Empty, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "L", "M", "N" };
		public static readonly List<Category> LIST_PRMRY_PYR_CD = ARR_PRMRY_PYR_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.cms.gov/Regulations-and-Guidance/Guidance/Transmittals/downloads/R470CP.pdf
		private static string[] ARR_CLM_CARR_PMT_DNL_CD = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110", "111", "112", "113", "114", "115", "116", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127", "128", "129", "130", "131", "132", "133", "134", "135", "136", "137", "138", "139", "140", "141", "142", "143", "144", "145", "146", "147", "148", "149", "150", "151", "152", "153", "154", "155", "156", "157", "158", "159", "160", "161", "162", "163", "164", "165", "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10", "B11", "B12", "B13", "B14", "B15", "B16", "B17", "B18", "B19", "B20", "B21", "B22", "B23", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10", "D11", "D12", "D13", "D14", "D15", "D16", "D17", "D18", "D19", "D20", "W1" };
		public static readonly List<Category> LIST_CLM_CARR_PMT_DNL_CD = ARR_CLM_CARR_PMT_DNL_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: https://www.resdac.org/cms-data/variables/Line-Processing-Indicator-Code
		private static string[] ARR_CLM_PRCSG_IND_CD = { "A", "B", "C", "D", "I", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "Z", "00", "12", "13", "14", "15", "16", "17", "18", "21", "22", "25", "26", "!", "@", "#", "$", "*", "(", ")", "+", "<", ">", "%", "&" };
		public static readonly List<Category> LIST_CLM_PRCSG_IND_CD = ARR_CLM_PRCSG_IND_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static string[] ARR_CLM_DISP_CD = { "01", "02", "03" };
		public static readonly List<Category> LIST_CLM_DISP_CD = ARR_CLM_DISP_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static int[] ARR_CCLF6_CLM_TYPE_CD = { 81, 82 };
		public static readonly List<Category> LIST_CCLF6_CLM_TYPE_CD = ARR_CCLF6_CLM_TYPE_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static string[] ARR_CCLF7_CLM_TYPE_CD = { "01", "02", "03", "04" };
		public static readonly List<Category> LIST_CCLF7_CLM_TYPE_CD = ARR_CCLF7_CLM_TYPE_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static string[] ARR_PRVDR_SRVC_ID_QLFYR_CD = { "01", "06", "07", "08", "11", "99" };
		public static readonly List<Category> LIST_PRVDR_SRVC_ID_QLFYR_CD = ARR_PRVDR_SRVC_ID_QLFYR_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static string[] ARR_CLM_DSPNSNG_STUS_CD = { "P", "C" };
		public static readonly List<Category> LIST_CLM_DSPNSNG_STUS_CD = ARR_CLM_DSPNSNG_STUS_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static string[] ARR_CLM_DAW_PROD_SLCTN_CD = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
		public static readonly List<Category> LIST_CLM_DAW_PROD_SLCTN_CD = ARR_CLM_DAW_PROD_SLCTN_CD.Select(a => new Category() { Value = a }).ToList();

		// Source: CCLF Info packet
		private static string[] ARR_PRVDR_PRSBNG_ID_QLFYR_CD = { "01", "06", "07", "08", "11", "12", "99" };
		public static readonly List<Category> LIST_PRVDR_PRSBNG_ID_QLFYR_CD = ARR_PRVDR_PRSBNG_ID_QLFYR_CD.Select(a => new Category() { Value = a }).ToList();
		// //////////////////////////////////////////////////

		#endregion

		#region HCPCS Utility

		private static List<Category> GetHCPCSCodes()
		{
			List<string> hcpcs = new List<string>();

			hcpcs.AddRange(GetHCPCSCodeSet("A", 21, 999));
			hcpcs.AddRange(GetHCPCSCodeSet("A", 4206, 8004));
			hcpcs.AddRange(GetHCPCSCodeSet("A", 9150, 9999));
			hcpcs.AddRange(GetHCPCSCodeSet("B", 4034, 9999));
			hcpcs.AddRange(GetHCPCSCodeSet("C", 1713, 9899));
			hcpcs.AddRange(GetHCPCSCodeSet("E", 100, 8002));
			hcpcs.AddRange(GetHCPCSCodeSet("G", 8, 9686));
			hcpcs.AddRange(GetHCPCSCodeSet("H", 1, 2037));
			hcpcs.AddRange(GetHCPCSCodeSet("J", 120, 9999));
			hcpcs.AddRange(GetHCPCSCodeSet("K", 1, 902));
			hcpcs.AddRange(GetHCPCSCodeSet("L", 112, 9900));
			hcpcs.AddRange(GetHCPCSCodeSet("M", 75, 301));
			hcpcs.AddRange(GetHCPCSCodeSet("P", 2028, 9615));
			hcpcs.AddRange(GetHCPCSCodeSet("Q", 35, 9980));
			hcpcs.AddRange(GetHCPCSCodeSet("R", 70, 76));
			hcpcs.AddRange(GetHCPCSCodeSet("S", 12, 9999));
			hcpcs.AddRange(GetHCPCSCodeSet("T", 1000, 5999));
			hcpcs.AddRange(GetHCPCSCodeSet("V", 2020, 2799));
			hcpcs.AddRange(GetHCPCSCodeSet("V", 5008, 5364));

			return hcpcs.Select(h => new Category() { Value = h }).ToList();
		}

		private static List<string> GetHCPCSCodeSet(string letterPrefix, int start, int end)
		{
			int count = end - (start - 1);

			List<string> result = Enumerable.Range(start, count).Select(a => letterPrefix + a.ToString().PadLeft(4, '0')).ToList();

			return result;
		}

		#endregion
	}
}
