
help:
	@echo "This will run a bunch of tests, use the command 'make test'"

debug:
	mkdir debug

test: debug
	cat examples/operations.1.txt | dotnet run --project Source/Console-App/ examples/input.1.json > debug/1-1.txt
	cat examples/operations.1.txt | dotnet run --project Source/Console-App/ examples/input.2.json > debug/1-2.txt
	cat examples/operations.2.txt | dotnet run --project Source/Console-App/ examples/input.1.json > debug/2-1.txt
	cat examples/operations.2.txt | dotnet run --project Source/Console-App/ examples/input.2.json > debug/2-2.txt

	-dotnet run --project Source/Console-App/ bad_file_name > debug/bad_file_name.txt
	-dotnet run --project Source/Console-App/ examples/operations.1.txt > debug/not_JSON.txt

clean:
	rm -rf Source/Console-App/obj
	rm -rf Source/Web-App/obj
	rm -rf build
	rm -rf debug