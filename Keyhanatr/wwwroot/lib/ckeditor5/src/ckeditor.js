/**
 * @license Copyright (c) 2014-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or https://ckeditor.com/legal/ckeditor-oss-license
 */
import ClassicEditor from '@ckeditor/ckeditor5-editor-classic/src/classiceditor.js';
import Autoformat from '@ckeditor/ckeditor5-autoformat/src/autoformat.js';
import BlockQuote from '@ckeditor/ckeditor5-block-quote/src/blockquote.js';
import Bold from '@ckeditor/ckeditor5-basic-styles/src/bold.js';
import Heading from '@ckeditor/ckeditor5-heading/src/heading.js';
import Image from '@ckeditor/ckeditor5-image/src/image.js';
import ImageUpload from '@ckeditor/ckeditor5-image/src/imageupload.js';
import Italic from '@ckeditor/ckeditor5-basic-styles/src/italic.js';
import Link from '@ckeditor/ckeditor5-link/src/link.js';
import Table from '@ckeditor/ckeditor5-table/src/table.js';
import TableToolbar from '@ckeditor/ckeditor5-table/src/tabletoolbar.js';
import ImageResize from '@ckeditor/ckeditor5-image/src/imageresize.js';
import SimpleUploadAdapter from '@ckeditor/ckeditor5-upload/src/adapters/simpleuploadadapter.js';
import CodeBlock from '@ckeditor/ckeditor5-code-block/src/codeblock.js';
import FontSize from '@ckeditor/ckeditor5-font/src/fontsize.js';
import FontColor from '@ckeditor/ckeditor5-font/src/fontcolor.js';
import Alignment from '@ckeditor/ckeditor5-alignment/src/alignment.js';
import Indent from '@ckeditor/ckeditor5-indent/src/indent.js';
import WordCount from '@ckeditor/ckeditor5-word-count/src/wordcount.js';
import Essentials from '@ckeditor/ckeditor5-essentials/src/essentials.js';
import Paragraph from '@ckeditor/ckeditor5-paragraph/src/paragraph.js';

export default class Editor extends ClassicEditor {}

// Plugins to include in the build.
Editor.builtinPlugins = [
	Autoformat,
	BlockQuote,
	Bold,
	Heading,
	Image,
	ImageUpload,
	Italic,
	Link,
	Table,
	TableToolbar,
	ImageResize,
	SimpleUploadAdapter,
	CodeBlock,
	FontSize,
	FontColor,
	Alignment,
	Indent,
	WordCount,
	Essentials,
	Paragraph
];

