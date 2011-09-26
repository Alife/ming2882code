/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function(config) {
    config.font_names = '宋体/宋体;黑体/黑体;仿宋/仿宋_GB2312;楷体/楷体_GB2312;隶书/隶书;幼圆/幼圆;' + config.font_names;
    config.toolbar = 'Full';
    config.toolbar_Full =
    [
        ['Source', '-', 'NewPage', 'Preview', '-', 'Templates'],
        ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
        ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
        ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
        '/',
        ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
        ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv'],
        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
        ['Link', 'Unlink', 'Anchor'],
        ['MyPhotoBtn', '-', 'Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
        '/',
        ['Styles', 'Format', 'Font', 'FontSize'],
        ['TextColor', 'BGColor'],
        ['Maximize', 'ShowBlocks']
    ];
    config.toolbar_Basic =
    [
        ['Bold', 'Italic', 'Underline', 'Strike', '-', 'TextColor', 'BGColor', 'StrikeThrough', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink'],
        ['Smiley', 'Image', '-', 'MyPhotoBtn'], ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
        '/', ['Styles', 'Format', 'Font', 'FontSize'], ['Source', 'Maximize']
    ];
    config.toolbar_Small =
    [
        ['Bold', 'Italic', 'Underline', 'Strike', '-', 'TextColor', 'BGColor', 'StrikeThrough', '-', 'Link', 'Unlink'], ['Smiley', 'Image', '-', 'MyPhotoBtn']
    ];
    config.extraPlugins = 'myphoto';
};
