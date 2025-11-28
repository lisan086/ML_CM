abp install-libs

cd ML.PCM && dotnet run --migrate-database && cd -


cd ML.PCM && dotnet dev-certs https -v -ep openiddict.pfx -p 6da313e9-fad8-454b-b656-b142bd4af430




exit 0