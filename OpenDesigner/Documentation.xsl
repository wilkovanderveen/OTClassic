<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsd="http://www.w3.org/2001/XMLSchema" >
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="xsd:schema">
    <html>
      <head>
        <style type="text/css">
          body
          {
          font-family: courier new;
          }

          span.element
          {
          font-weight:bold;
          }

          div.element
          {
          margin-left: 20px;
          }
        </style>
        <title>

        </title>
      </head>
      <body>
        <xsl:apply-templates  />
      </body>
    </html>
  </xsl:template>

  <xsl:template match="xsd:element[not(@type)]">
    <xsl:variable name="ElementType">
      <xsl:value-of select="@type" />      
    </xsl:variable>
 
    <div class="element">
      <span class="element">
        <xsl:text>&lt;</xsl:text>
        <xsl:value-of select="@name"/>
      </span>
      <xsl:for-each select="xsd:complexType/xsd:attribute">
        <xsl:apply-templates />
      </xsl:for-each>
      <xsl:text>&gt;</xsl:text>

      <xsl:apply-templates />

      <xsl:text>&lt;/</xsl:text>
      <xsl:value-of select="@name"/>
      <xsl:text>&gt;</xsl:text>
    </div>
  </xsl:template>

  <xsl:template match="xsd:attribute">
    <span class="attribute">
      <xsl:text> </xsl:text>
      <xsl:if test="@use != 'required'">
        <xsl:text>[</xsl:text>
      </xsl:if>
      <xsl:value-of select="@name" />
      <xsl:text>=</xsl:text>
      <xsl:choose>
        <xsl:when test="xsd:simpleType">
          <xsl:if test="xsd:simpleType/xsd:restriction">
            <xsl:value-of select="xsd:restriction/@base"/>
          </xsl:if>
        </xsl:when>
        <xsl:when test="@type = 'xsd:string'">
          <xsl:text>string</xsl:text>
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="@type"/>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:if test="@use != 'required'">
        <xsl:text>]</xsl:text>
      </xsl:if>
    </span>
  </xsl:template>

  <xsl:template match="xsd:restriction">
    <xsl:if test="xsd:minInclusive">
      <xsl:value-of select="xsd:minInclusive/@value"/>
      <xsl:if test="xsd:maxInclusive">
        <xsl:text>-</xsl:text>
      </xsl:if>
    </xsl:if>
    <xsl:if test="xsd:maxInclusive">
      <xsl:value-of select="xsd:maxInclusive/@value"/>
    </xsl:if>
  </xsl:template>

</xsl:stylesheet>
