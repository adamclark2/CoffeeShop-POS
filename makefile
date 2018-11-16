
help:
	@echo "This will run a bunch of tests, use the command 'make test'"

debug:
	mkdir debug

test: debug
	cat examples/operations.1.txt | dotnet run --project Source/ examples/input.1.json > debug/1-1.txt
	cat examples/operations.2.txt | dotnet run --project Source/ examples/input.2.json > debug/2-2.txt
	cat examples/operations.3.txt | dotnet run --project Source/ examples/input.3.json > debug/3-3.txt
	cat examples/operations.4.txt | dotnet run --project Source/ examples/input.4.json > debug/4-4.txt
	cat examples/operations.5.txt | dotnet run --project Source/ examples/input.5.json > debug/5-5.txt
	cat examples/operations.6.txt | dotnet run --project Source/ examples/input.6.json > debug/6-6.txt

	-dotnet run --project Source/ bad_file_name > debug/bad_file_name.txt
	-dotnet run --project Source/ examples/operations.1.txt > debug/not_JSON.txt

clean:
	rm -rf Source/obj
	rm -rf build
	rm -rf debug
	rm -rf outputs
	rm -rf Source/outputs