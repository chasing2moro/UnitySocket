

namespace twp {
 namespace app {
  namespace city {


   //
   // 常量定义
   //
   public enum EAreaDefLimit
   {
    LIMIT_MAX_AREA_COUNT = 6,  // 区域最大数

    LIMIT_MIN_ROW_COUNT = 0,   // 区域最小行
    LIMIT_MIN_COL_COUNT = 0,   // 区域最小列

    LIMIT_MAX_ROW_COUNT = 139, // 区域最大行
    LIMIT_MAX_COL_COUNT = 139, // 区域最大列

    LIMIT_SHOW_AREA_ROW = 2,   // 显示的区域行
    LIMIT_SHOW_AREA_COL = 2,   // 显示的区域列

    BEGIN_AREA_ROW = 69,       // 分派城市时的起始行
    BEGIN_AREA_COL = 69,       // 分配城市时的起始列
   };

  public enum AreaOperationType : byte
   {
    AREA_OPERATION_TYPE_SHOW = 0, // 显示周边区域城市数据
   };

   public enum AreaConstructDir : byte
   {
    AREA_CONSTRUCT_DIR_LEFT = 0,
    AREA_CONSTRUCT_DIR_UP = 1,
    AREA_CONSTRUCT_DIR_RIGHT = 2,
    AREA_CONSTRUCT_DIR_DOWN = 3,
   };

   public class AreaInfoDB 
   {
    public byte area;

    public byte begin_row; // 起始的行
    public byte begin_col; // 起始的列

    public byte row; // 当前分配的行
    public byte col; // 当前分配的列

    public AreaConstructDir dir;
				
	//public void FromBin
   };


  }
 }
}



