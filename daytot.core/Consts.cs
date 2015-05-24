using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daytot.core
{
    public class Consts
    {
        /*Loại tài khoản trên nhệ thống*/
        public const int ACCOUN_TYPE_STUDENT = 1;
        public const int ACCOUN_TYPE_TEACHER = 1024;
        public const int ACCOUN_TYPE_DEAN = 2048;
        public const int ACCOUN_TYPE_ADMIN = 65536;

        /*Trạng thái kích hoạt tài khoản*/
        public const int ACCOUN_STATUS_UNACTIVE = 0;
        public const int ACCOUN_STATUS_VERIFY_EMAIL = 2;
        public const int ACCOUN_STATUS_VERIFY_PHONE = 4;
        public const int ACCOUN_STATUS_VERIFY_CMND = 8;

        /*Loại đối tượng*/
        public const int REFERTYPE_SUBJECT = 1;
        public const int REFERTYPE_COURSE = 2;
        public const int REFERTYPE_LECTURE = 3;
        public const int REFERTYPE_USER = 4;


        /*Chat room*/
        public const string CHATROOM_LECTURE = "CHATROOM_LECTURE";
        public const string CHATROOM_WHITEBOARD = "CHATROOM_WHITEBOARD";

        /*Ask status*/
        public const long ASK_STATUS_OPEN = 1;
        public const long ASK_STATUS_CLOSED = 512;

        /*Loại Media*/
        public const int MEDIA_TYPE_IMAGE = 1;
        public const int MEDIA_TYPE_EXTERNALIMAGE = 2;
        public const int MEDIA_TYPE_VIDEO = 3;
        public const int MEDIA_TYPE_YOUTUBE = 4;
        public const int MEDIA_TYPE_VIMEO = 5;
        public const int MEDIA_TYPE_PDF = 6;
        public const int MEDIA_TYPE_PPT = 7;

        /*Cách sử dụng Media*/
        public const int MEDIA_USAGE_AVATAR = 1;
        public const int MEDIA_USAGE_COURSEINTRO = 2;

        /*Loại khóa học*/
        public const int COURSE_TYPE_FREE = 0;
        public const int COURSE_TYPE_PAID = 1;
        public const int COURSE_TYPE_PRIVATE = 2;

        /*Trang thái*/
        /// <summary>
        /// Trạng thái khởi tạo
        /// </summary>
        public const long STATUS_INIT = 1;
        /// <summary>
        /// Trạng thái đang soạn
        /// </summary>
        public const long STATUS_DRAFT = 2;
        /// <summary>
        /// Trạng thái chờ duyệt
        /// </summary>
        public const long STATUS_WAITFORAPPROVE = 4;
        /// <summary>
        /// Trạng thái nội dung được duyệt
        /// </summary>
        public const long STATUS_APPROVED = 8;
        /// <summary>
        /// Trạng thái xóa
        /// </summary>
        public const long STATUS_DELETED = 16;
        /// <summary>
        /// Từ chối
        /// </summary>
        public const int STATUS_REJECTED = 32;
        /// <summary>
        /// Trạng thái chấp nhận cho phép dạy từ daytot
        /// </summary>
        public const long STATUS_ACCEPTED = 64;
        /// <summary>
        /// Trạng thái công khai của người dùng
        /// </summary>
        public const long STATUS_PUBLISHED = 128;
        /// <summary>
        /// Tiêu biểu
        /// </summary>
        public const int STATUS_REPRESENTATIVE = 256;


        /*Kích thước hình*/
        public const int UPLOAD_IMAGE_MAXWIDTH = 1280;
        public const int UPLOAD_IMAGE_MAXHEIGHT = 720;

        /*Giá trị mặc định*/
        public const string DEFAULT_INTRO_IMAGE = @"def/intro-image.jpg";
        public const string DEFAULT_AVATAR_IMAGE = @"def/avatar.jpg";

        /*Giá trị cho vai trò tham gia giảng dạy*/
        public const int TEACHER_ROLE_MAIN = 1;
        public const int TEACHER_ROLE_HOMEWORK = 2;

        /*Giá trị tính thời gian cho một câu hỏi bài tập*/
        public const int HOMEWORK_TIMEUP_PER_QUESTION = 2;

        /*Số bài giảng tối thiều để công khai một khóa học miễn phí*/
        public const int COURSE_FREE_MIN_LECTURE_TO_PUBLISH = 3;
        public const int COURSE_PAID_MIN_LECTURE_TO_PUBLISH = 3;

        /*Loại bài thi*/
        public const int QUIZ_HOMEWORK = 1;
        public const int QUIZ_EXAM = 0;
    }
}
