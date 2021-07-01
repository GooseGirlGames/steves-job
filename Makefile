VERSION = $(shell git describe --always --tags)

.PHONY: prep-dirs
prep-dirs: rm-dirs
	mkdir -p build/steves-job-win
	cp doc/Manual.md build/steves-job-win
	echo $(VERSION) >> build/steves-job-win/version
	mkdir -p build/steves-job-linux
	cp doc/Manual.md build/steves-job-linux
	echo $(VERSION) >> build/steves-job-linux/version

.PHONY: rm-dirs
rm-dirs:
	rm -fr build

.PHONY: zip
zip:
	cd build && zip -r steves-job-$(VERSION)-win.zip steves-job-win
	cd build && zip -r steves-job-$(VERSION)-linux.zip steves-job-linux
