namespace Script.Model
{
    public static class EvmChain
    {
        static EvmChain()
        {
            ETHEREUM = new ChainInformation
                       {
                           Title = "Ethereum",
                           Symbol = "ETH",
                           MainnetContractAddress = "your smart contract address",
                           TestnetContractAddress = "your smart contract address",
                           MainnetRpcUrl = "your rpc url",
                           TestnetRpcUrl = "your rpc url",
                           MainnetExplorerDomain = "etherscan.io",
                           TestnetExplorerDomain = "rinkeby.etherscan.io",
                           MainnetExplorerApiUrl = "https://api-rinkeby.etherscan.io",
                           TestnetExplorerApiUrl = "https://api-rinkeby.etherscan.io",
                       };
            
            BNB_CHAIN = new ChainInformation
                        {
                            Title = "BNB Chain",
                            Symbol = "BNB",
                            MainnetContractAddress = "your smart contract address",
                            TestnetContractAddress = "your smart contract address",
                            MainnetRpcUrl = "https://bsc-dataseed.binance.org",
                            TestnetRpcUrl = "https://data-seed-prebsc-1-s1.binance.org:8545",
                            MainnetExplorerDomain = "bscscan.com",
                            TestnetExplorerDomain = "testnet.bscscan.com",
                            MainnetExplorerApiUrl = "https://api.bscscan.com",
                            TestnetExplorerApiUrl = "https://api-testnet.bscscan.com"
                        };

            POLYGON = new ChainInformation
                      {
                          Title = "Polygon",
                          Symbol = "MATIC",
                          MainnetContractAddress = "your smart contract address",
                          TestnetContractAddress = "your smart contract address",
                          MainnetRpcUrl = "your rpc url",
                          TestnetRpcUrl = "your rpc url",
                          MainnetExplorerDomain = "polygonscan.com",
                          TestnetExplorerDomain = "mumbai.polygonscan.com",
                          MainnetExplorerApiUrl = "https://api.polygonscan.com",
                          TestnetExplorerApiUrl= "https://api-testnet.polygonscan.com",
                      };
            
            AVALANCHE = new ChainInformation
                        {
                            Title = "Avalanche",
                            Symbol = "AVAX",
                            MainnetContractAddress = "your smart contract address",
                            TestnetContractAddress = "your smart contract address",
                            MainnetRpcUrl = "https://api.avax.network/ext/bc/C/rpc",
                            TestnetRpcUrl = "https://api.avax-test.network/ext/bc/C/rpc",
                            MainnetExplorerDomain = "snowtrace.io",
                            TestnetExplorerDomain = "testnet.snowtrace.io",
                            MainnetExplorerApiUrl = "https://api.snowtrace.io/",
                            TestnetExplorerApiUrl = "https://api-testnet.snowtrace.io/"
                        };
            BLT = new ContractInformation
                  {
                      MainnetContractAddress = "0xfB0727386DB1A630344a08467b45541bEC9bCf17",
                      TestnetContractAddress = "0xfB0727386DB1A630344a08467b45541bEC9bCf17",
                  };
        }
        
        public static ChainInformation ETHEREUM { get; set; }

        public static ChainInformation BNB_CHAIN { get; set; }
        
        public static ChainInformation POLYGON { get; set; }
        
        public static ChainInformation AVALANCHE { get; set; }
        
        public static ContractInformation BLT { get; set; }
    }
}