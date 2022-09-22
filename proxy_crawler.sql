--
-- Banco de dados: `proxy_crawler`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `crawlerinfo`
--

CREATE TABLE `crawlerinfo` (
  `Id` int(11) UNSIGNED NOT NULL,
  `StartCrawlerPages` datetime DEFAULT NULL,
  `EndCrawlerPages` datetime DEFAULT NULL,
  `TotalRowsPages` int(11) DEFAULT NULL,
  `TotalPages` int(11) NOT NULL,
  `JsonProxysInfo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL CHECK (json_valid(`JsonProxysInfo`))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `crawlerinfo`
--
ALTER TABLE `crawlerinfo`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `crawlerinfo`
--
ALTER TABLE `crawlerinfo`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
COMMIT;
