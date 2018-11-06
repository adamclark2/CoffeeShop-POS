
help:
	@echo "This will run a bunch of tests, use the command 'make test'"

debug:
	mkdir debug

test: debug
	dotnet run --project Source/Console-App/ examples/input.1.json > debug/1.txt
	dotnet run --project Source/Console-App/ examples/input.2.json > debug/2.txt