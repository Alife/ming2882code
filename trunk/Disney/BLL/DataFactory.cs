using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlServerDAL;

namespace BLL
{
    public class DataFactory
    {
        private static web_PhotoTypeData _web_PhotoTypeData;
        public static web_PhotoTypeData web_PhotoTypeData()
        {
            if (_web_PhotoTypeData == null)
                _web_PhotoTypeData = new web_PhotoTypeData();
            return _web_PhotoTypeData;
        }
        private static t_UserData _memberData;
        public static t_UserData t_UserData()
        {
            if (_memberData == null)
                _memberData = new t_UserData();
            return _memberData;
        }
        private static t_UserInfoData _MemberSettingData;
        public static t_UserInfoData t_UserInfoData()
        {
            if (_MemberSettingData == null)
                _MemberSettingData = new t_UserInfoData();
            return _MemberSettingData;
        }
        private static t_UserTypeData _t_UserTypeData;
        public static t_UserTypeData t_UserTypeData()
        {
            if (_t_UserTypeData == null)
                _t_UserTypeData = new t_UserTypeData();
            return _t_UserTypeData;
        }
        private static t_UserPointData _t_UserPointData;
        public static t_UserPointData t_UserPointData()
        {
            if (_t_UserPointData == null)
                _t_UserPointData = new t_UserPointData();
            return _t_UserPointData;
        }
        private static t_UserAddressData _t_UserAddressData;
        public static t_UserAddressData t_UserAddressData()
        {
            if (_t_UserAddressData == null)
                _t_UserAddressData = new t_UserAddressData();
            return _t_UserAddressData;
        }
        private static sys_ApplicationData _sys_ApplicationData;
        public static sys_ApplicationData sys_ApplicationData()
        {
            if (_sys_ApplicationData == null)
                _sys_ApplicationData = new sys_ApplicationData();
            return _sys_ApplicationData;
        }
        private static sys_RoleData _sys_RoleData;
        public static sys_RoleData sys_RoleData()
        {
            if (_sys_RoleData == null)
                _sys_RoleData = new sys_RoleData();
            return _sys_RoleData;
        }
        private static sys_UserRoleData _sys_UserRoleData;
        public static sys_UserRoleData sys_UserRoleData()
        {
            if (_sys_UserRoleData == null)
                _sys_UserRoleData = new sys_UserRoleData();
            return _sys_UserRoleData;
        }
        private static sys_OperationData _sys_OperationData;
        public static sys_OperationData sys_OperationData()
        {
            if (_sys_OperationData == null)
                _sys_OperationData = new sys_OperationData();
            return _sys_OperationData;
        }
        private static sys_FieldData _sys_FieldData;
        public static sys_FieldData sys_FieldData()
        {
            if (_sys_FieldData == null)
                _sys_FieldData = new sys_FieldData();
            return _sys_FieldData;
        }
        private static sys_PermissionData _sys_PermissionData;
        public static sys_PermissionData sys_PermissionData()
        {
            if (_sys_PermissionData == null)
                _sys_PermissionData = new sys_PermissionData();
            return _sys_PermissionData;
        }
        private static sys_PermissionFieldData _sys_PermissionFieldData;
        public static sys_PermissionFieldData sys_PermissionFieldData()
        {
            if (_sys_PermissionFieldData == null)
                _sys_PermissionFieldData = new sys_PermissionFieldData();
            return _sys_PermissionFieldData;
        }
        private static sys_DataPermissionData _sys_DataPermissionData;
        public static sys_DataPermissionData sys_DataPermissionData()
        {
            if (_sys_DataPermissionData == null)
                _sys_DataPermissionData = new sys_DataPermissionData();
            return _sys_DataPermissionData;
        }
        private static sys_SerialNumberData _sys_SerialNumberData;
        public static sys_SerialNumberData sys_SerialNumberData()
        {
            if (_sys_SerialNumberData == null)
                _sys_SerialNumberData = new sys_SerialNumberData();
            return _sys_SerialNumberData;
        }
        private static sys_AreaData _sys_AreaData;
        public static sys_AreaData sys_AreaData()
        {
            if (_sys_AreaData == null)
                _sys_AreaData = new sys_AreaData();
            return _sys_AreaData;
        }
        private static PageData _PageData;
        public static PageData PageData()
        {
            if (_PageData == null)
                _PageData = new PageData();
            return _PageData;
        }
        private static sys_LinkData _sys_LinkData;
        public static sys_LinkData sys_LinkData()
        {
            if (_sys_LinkData == null)
                _sys_LinkData = new sys_LinkData();
            return _sys_LinkData;
        }
        private static ArticleCategoryData _ArticleCategoryData;
        private static ArticleData _ArticleData;
        private static ArticleFileData _ArticleFileData;
        private static ArticleCommentData _ArticleCommentData;
        private static ArticleDotData _ArticleDotData;
        private static ArticleTopData _ArticleTopData;
        public static ArticleCategoryData ArticleCategoryData()
        {
            if (_ArticleCategoryData == null)
                _ArticleCategoryData = new ArticleCategoryData();
            return _ArticleCategoryData;
        }
        public static ArticleData ArticleData()
        {
            if (_ArticleData == null)
                _ArticleData = new ArticleData();
            return _ArticleData;
        }
        public static ArticleFileData ArticleFileData()
        {
            if (_ArticleFileData == null)
                _ArticleFileData = new ArticleFileData();
            return _ArticleFileData;
        }
        public static ArticleCommentData ArticleCommentData()
        {
            if (_ArticleCommentData == null)
                _ArticleCommentData = new ArticleCommentData();
            return _ArticleCommentData;
        }
        public static ArticleDotData ArticleDotData()
        {
            if (_ArticleDotData == null)
                _ArticleDotData = new ArticleDotData();
            return _ArticleDotData;
        }
        public static ArticleTopData ArticleTopData()
        {
            if (_ArticleTopData == null)
                _ArticleTopData = new ArticleTopData();
            return _ArticleTopData;
        }
        private static SqlServerDAL.sys_LogData _sys_LogData;
        public static SqlServerDAL.sys_LogData sys_LogData()
        {
            if (_sys_LogData == null)
                _sys_LogData = new SqlServerDAL.sys_LogData();
            return _sys_LogData;
        }
        private static sys_LogCategoryData _sys_LogCategoryData;
        public static sys_LogCategoryData sys_LogCategoryData()
        {
            if (_sys_LogCategoryData == null)
                _sys_LogCategoryData = new sys_LogCategoryData();
            return _sys_LogCategoryData;
        }
        private static sys_LogOpData _sys_LogOpData;
        public static sys_LogOpData sys_LogOpData()
        {
            if (_sys_LogOpData == null)
                _sys_LogOpData = new sys_LogOpData();
            return _sys_LogOpData;
        }
        private static OrderData _OrderData;
        private static OrderProductData _OrderProductData;
        private static ProductCategoryData _ProductCategoryData;
        private static ProductCommentData _ProductCommentData;
        private static ProductData _ProductData;
        private static ProductFileData _ProductFileData;
        private static ShippingData _ShippingData;
        public static OrderData OrderData()
        {
            if (_OrderData == null)
                _OrderData = new OrderData();
            return _OrderData;
        }
        public static OrderProductData OrderProductData()
        {
            if (_OrderProductData == null)
                _OrderProductData = new OrderProductData();
            return _OrderProductData;
        }
        public static ProductCategoryData ProductCategoryData()
        {
            if (_ProductCategoryData == null)
                _ProductCategoryData = new ProductCategoryData();
            return _ProductCategoryData;
        }
        public static ProductCommentData ProductCommentData()
        {
            if (_ProductCommentData == null)
                _ProductCommentData = new ProductCommentData();
            return _ProductCommentData;
        }
        public static ProductData ProductData()
        {
            if (_ProductData == null)
                _ProductData = new ProductData();
            return _ProductData;
        }
        public static ProductFileData ProductFileData()
        {
            if (_ProductFileData == null)
                _ProductFileData = new ProductFileData();
            return _ProductFileData;
        }
        public static ShippingData ShippingData()
        {
            if (_ShippingData == null)
                _ShippingData = new ShippingData();
            return _ShippingData;
        }
        private static MessageData _MessageData;
        public static MessageData MessageData()
        {
            if (_MessageData == null)
                _MessageData = new MessageData();
            return _MessageData;
        }
        private static w_PhotoData _w_PhotoData;
        public static w_PhotoData w_PhotoData()
        {
            if (_w_PhotoData == null)
                _w_PhotoData = new w_PhotoData();
            return _w_PhotoData;
        }
        private static w_PhotoCategoryData _w_PhotoCategoryData;
        public static w_PhotoCategoryData w_PhotoCategoryData()
        {
            if (_w_PhotoCategoryData == null)
                _w_PhotoCategoryData = new w_PhotoCategoryData();
            return _w_PhotoCategoryData;
        }
        private static d_DepartmentData _d_DepartmentData;
        public static d_DepartmentData d_DepartmentData()
        {
            if (_d_DepartmentData == null)
                _d_DepartmentData = new d_DepartmentData();
            return _d_DepartmentData;
        }
        private static d_ClassTypeData _d_ClassTypeData;
        public static d_ClassTypeData d_ClassTypeData()
        {
            if (_d_ClassTypeData == null)
                _d_ClassTypeData = new d_ClassTypeData();
            return _d_ClassTypeData;
        }
        private static d_CostumeData _d_CostumeData;
        public static d_CostumeData d_CostumeData()
        {
            if (_d_CostumeData == null)
                _d_CostumeData = new d_CostumeData();
            return _d_CostumeData;
        }
        private static d_CoverTypeData _d_CoverTypeData;
        public static d_CoverTypeData d_CoverTypeData()
        {
            if (_d_CoverTypeData == null)
                _d_CoverTypeData = new d_CoverTypeData();
            return _d_CoverTypeData;
        }
        private static d_KitTemplateData _d_KitTemplateData;
        public static d_KitTemplateData d_KitTemplateData()
        {
            if (_d_KitTemplateData == null)
                _d_KitTemplateData = new d_KitTemplateData();
            return _d_KitTemplateData;
        }
        private static d_InsideMaterialData _d_InsideMaterialData;
        public static d_InsideMaterialData d_InsideMaterialData()
        {
            if (_d_InsideMaterialData == null)
                _d_InsideMaterialData = new d_InsideMaterialData();
            return _d_InsideMaterialData;
        }
        private static d_InsideTypeData _d_InsideTypeData;
        public static d_InsideTypeData d_InsideTypeData()
        {
            if (_d_InsideTypeData == null)
                _d_InsideTypeData = new d_InsideTypeData();
            return _d_InsideTypeData;
        }
        private static d_KitTypeData _d_KitTypeData;
        public static d_KitTypeData d_KitTypeData()
        {
            if (_d_KitTypeData == null)
                _d_KitTypeData = new d_KitTypeData();
            return _d_KitTypeData;
        }
        private static d_KitPhotoTypeData _d_KitPhotoTypeData;
        public static d_KitPhotoTypeData d_KitPhotoTypeData()
        {
            if (_d_KitPhotoTypeData == null)
                _d_KitPhotoTypeData = new d_KitPhotoTypeData();
            return _d_KitPhotoTypeData;
        }
        private static d_ArtistPriceData _d_ArtistPriceData;
        public static d_ArtistPriceData d_ArtistPriceData()
        {
            if (_d_ArtistPriceData == null)
                _d_ArtistPriceData = new d_ArtistPriceData();
            return _d_ArtistPriceData;
        }
        private static d_KitData _d_KitData;
        public static d_KitData d_KitData()
        {
            if (_d_KitData == null)
                _d_KitData = new d_KitData();
            return _d_KitData;
        }
        private static d_KitWorkData _d_KitWorkData;
        public static d_KitWorkData d_KitWorkData()
        {
            if (_d_KitWorkData == null)
                _d_KitWorkData = new d_KitWorkData();
            return _d_KitWorkData;
        }
        private static d_KitPhotoData _d_KitPhotoData;
        public static d_KitPhotoData d_KitPhotoData()
        {
            if (_d_KitPhotoData == null)
                _d_KitPhotoData = new d_KitPhotoData();
            return _d_KitPhotoData;
        }
        private static d_KitClassData _d_KitClassData;
        public static d_KitClassData d_KitClassData()
        {
            if (_d_KitClassData == null)
                _d_KitClassData = new d_KitClassData();
            return _d_KitClassData;
        }
        private static d_ConfirmPhotoData _d_ConfirmPhotoData;
        public static d_ConfirmPhotoData d_ConfirmPhotoData()
        {
            if (_d_ConfirmPhotoData == null)
                _d_ConfirmPhotoData = new d_ConfirmPhotoData();
            return _d_ConfirmPhotoData;
        }
        private static d_KitChildData _d_KitChildData;
        public static d_KitChildData d_KitChildData()
        {
            if (_d_KitChildData == null)
                _d_KitChildData = new d_KitChildData();
            return _d_KitChildData;
        }
        private static d_KitCostumeData _d_KitCostumeData;
        public static d_KitCostumeData d_KitCostumeData()
        {
            if (_d_KitCostumeData == null)
                _d_KitCostumeData = new d_KitCostumeData();
            return _d_KitCostumeData;
        }
        private static d_KitQuestionData _d_KitQuestionData;
        public static d_KitQuestionData d_KitQuestionData()
        {
            if (_d_KitQuestionData == null)
                _d_KitQuestionData = new d_KitQuestionData();
            return _d_KitQuestionData;
        }
        private static d_ArtistMonthData _d_ArtistMonthData;
        public static d_ArtistMonthData d_ArtistMonthData()
        {
            if (_d_ArtistMonthData == null)
                _d_ArtistMonthData = new d_ArtistMonthData();
            return _d_ArtistMonthData;
        }
        private static d_TotolMonthData _d_TotolMonthData;
        public static d_TotolMonthData d_TotolMonthData()
        {
            if (_d_TotolMonthData == null)
                _d_TotolMonthData = new d_TotolMonthData();
            return _d_TotolMonthData;
        }
        private static ReportData _ReportData;
        public static ReportData ReportData()
        {
            if (_ReportData == null)
                _ReportData = new ReportData();
            return _ReportData;
        }
        private static d_KitPhotoReturnData _d_KitPhotoReturnData;
        public static d_KitPhotoReturnData d_KitPhotoReturnData()
        {
            if (_d_KitPhotoReturnData == null)
                _d_KitPhotoReturnData = new d_KitPhotoReturnData();
            return _d_KitPhotoReturnData;
        }
    }
}
