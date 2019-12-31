using System;
namespace BindingSample.Models
{
    /// <summary>
    /// 安否情報
    /// </summary>
    public class Safety
    {
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDateTime;

        /// <summary>
        /// 本人状態
        /// </summary>
        public EStatus Status;

        /// <summary>
        /// 出社可否
        /// </summary>
        public bool CanWork;

        /// <summary>
        /// 尾行
        /// </summary>
        public string Remarks;

        /// <summary>
        /// 本人状態種別
        /// </summary>
        public enum EStatus
        {
            /// <summary>
            /// 安全
            /// </summary>
            Safe,

            /// <summary>
            /// 怪我
            /// </summary>
            Injury,

            /// <summary>
            /// 病気
            /// </summary>
            Sick,
        }

        public Safety()
        {
        }
    }
}
