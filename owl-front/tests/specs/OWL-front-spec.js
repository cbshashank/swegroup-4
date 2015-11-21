/**
 * Created by Scott on 10/7/2015.
 */
//There are multiple selections titled: �Location�, �Setting�, �Color�, �Shape�, �Texture�, �Smell�, and �Pattern�.
define( ['SystemInputs','ClientCommunicationModule'], function(SystemInputs, ClientCommunicationModule) {
    input = new SystemInputs();

    describe("System input testing", function() {

        it('outputs a selected location', function () {
            expect(input.getInput("Location")).toEqual("Location:Western_US");
        });

        it('outputs a selected setting', function() {
            expect(input.getInput("Setting")).toEqual("Setting:Rural");
        });

        it('outputs a selected color', function() {
            expect(input.getInput("Color")).toEqual("Color:Red");
        });

        it('outputs a selected shape', function() {
            expect(input.getInput("Shape")).toEqual("Shape:Vine");
        });

        it('outputs a selected texture', function() {
            expect(input.getInput("Texture")).toEqual("Texture:Rough");
        });

        it('outputs a selected smell', function() {
            expect(input.getInput("Smell")).toEqual("Smell:Sweet");
        });

        it('outputs a selected pattern', function() {
            expect(input.getInput("Pattern")).toEqual("Pattern:Clusters");
        });

    });

    describe("Client communication module testing", function() {
        it('outputs a selected color', function() {
            expect(input.getInput("Color")).toEqual("Color:Red");
        });

    });

    describe("Result display testing", function() {
        it('outputs a selected color', function() {
            expect(input.getInput("Color")).toEqual("Color:Red");
        });
    });

});