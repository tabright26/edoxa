import { Component } from "react";
import { ReactWrapper } from "enzyme";

Object.defineProperty(ReactWrapper.prototype, "findFieldByName", {
  value: function findFieldByName(fieldName: string): Component {
    return this.find(`Field[name="${fieldName}"]`).first();
  },
  writable: true,
  configurable: true
});

Object.defineProperty(ReactWrapper.prototype, "findInputByName", {
  value: function findInputByName(inputName: string): Component {
    return this.find(`input[name="${inputName}"]`).first();
  },
  writable: true,
  configurable: true
});

Object.defineProperty(ReactWrapper.prototype, "findSaveButton", {
  value: function findSaveButton(): Component {
    return this.find(`SaveButton`)
      .find(`button`)
      .first();
  },
  writable: true,
  configurable: true
});

Object.defineProperty(ReactWrapper.prototype, "findCancelButton", {
  value: function findCancelButton(): Component {
    return this.find(`CancelButton`)
      .find(`button`)
      .first();
  },
  writable: true,
  configurable: true
});

Object.defineProperty(ReactWrapper.prototype, "findSubmitButton", {
  value: function findSubmitButton(): Component {
    return this.find(`SubmitButton`)
      .find(`button`)
      .first();
  },
  writable: true,
  configurable: true
});

Object.defineProperty(ReactWrapper.prototype, "findFormFeedback", {
  value: function findFormFeedback(message: string): boolean {
    //console.log(this.find("FormFeedback").debug());
    return this.find(`FormFeedback`).someWhere(node => node.text() === message);
  },
  writable: true,
  configurable: true
});
