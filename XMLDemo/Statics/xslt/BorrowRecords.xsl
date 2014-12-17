﻿<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <body>
        <h2>借阅记录查询</h2>
        <table border="1">
          <tr bgcolor="#9acd32">
            <th align="left">读者编号</th>
            <th align="left">图书编号</th>
            <th align="left">借书日期</th>
            <th align="left">还书日期</th>
          </tr>
          
          <xsl:for-each select="Records/Record">
            <tr>
              <td>
                <xsl:value-of select="ReaderID"/>
              </td>
              <td>
                <xsl:value-of select="BookID"/>
              </td>
              <td>
                <xsl:value-of select="BorrowDate"/>
              </td>
              <td>
                <xsl:value-of select="ReturnDate"/>
              </td>
            </tr>
          </xsl:for-each>
          
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>