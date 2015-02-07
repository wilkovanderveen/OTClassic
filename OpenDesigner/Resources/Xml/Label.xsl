<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
	<xsl:output method="xml" indent="yes"/>

	<xsl:variable name="PageWidth"></xsl:variable>
	<xsl:variable name="PageHeight"></xsl:variable>
	
	<xsl:variable name="PageMarginTop"></xsl:variable>
	<xsl:variable name="PageMarginLeft"></xsl:variable>
	<xsl:variable name="PageMarginBottom"></xsl:variable>
	<xsl:variable name="PageMarginRight"></xsl:variable>

	<xsl:variable name="LabelWidth"></xsl:variable>
	<xsl:variable name="LabelHeight"></xsl:variable>
	<xsl:variable name="LabelMarginHorizontal"></xsl:variable>
	<xsl:variable name="LabelMarginVertical"></xsl:variable>

	<xsl:variable name="LabelTopMargin"></xsl:variable>
	<xsl:variable name="LabelLeftMargin"></xsl:variable>
	
	<xsl:template match="document">
		<template>
			<metadata></metadata>
		</template>
	</xsl:template>
</xsl:stylesheet>
