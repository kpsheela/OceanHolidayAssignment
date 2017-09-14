$(document).ready(function () {
    //var algolia = new AlgoliaSearch('EX5G7ARG1S', 'bc8ffe47f2c6a78b0a70758ca686fa48');
    //var index = algolia.initIndex('get_hotels');

    const search = instantsearch({
        appId: 'WZPTW6FGWV',
        apiKey: '5b25934a9ebe3e6ae36aa82d2e194e92',
        indexName: 'get_hotels',
        urlSync: true
    });

    search.addWidget(
        instantsearch.widgets.searchBox({
            container:'#search-input'
        })
    );

    search.addWidget(
        instantsearch.widgets.hits({
            container: '#hits',
            hitsPerPage: 10,
            templates: {
                item: document.getElementById('hit-template').innerHTML,
                empty: "We did not find any results for the search <em> \"{{query}}\"</em>"
            }
        })
    );
    search.addWidget(
        instantsearch.widgets.pagination({
            container: '#pagination'
        })
    );
    search.start();
})