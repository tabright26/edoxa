import React from "react";
import { Provider } from "react-redux";
import { ReactWrapper } from "enzyme";
import Create from "./Create";
import { configureStore } from "store";
import { FormGroup } from "reactstrap";
import Input from "components/Shared/Input";
import {
  PERSONALINFO_FIRSTNAME_REQUIRED,
  PERSONALINFO_FIRSTNAME_INVALID,
  PERSONALINFO_LASTNAME_REQUIRED,
  PERSONALINFO_LASTNAME_INVALID
} from "validation";
import { create } from "istanbul-reports";

const shallow = global["shallow"];
const mount = global["mount"];

const initialState: any = {};
const store = configureStore(initialState);

const createWrapper = (): ReactWrapper | any => {
  return mount(
    <Provider store={store}>
      <Create />
    </Provider>
  );
};

describe("<UserInformationCreateForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(<Create />);
    expect(shallowWrapper).toMatchSnapshot();
  });

  describe("defines information create form fields", () => {
    test.each([
      ["firstName", "Enter your first name"],
      ["lastName", "Enter your last name"]
    ])("renders name fields", (name: string, label: string) => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName(name);

      expect(field.prop("label")).toBe(label);
      expect(field.prop("component")).toBe(Input.Text);
    });

    test.each([["year"], ["month"], ["day"]])(
      "renders dob fields",
      (name: string) => {
        const wrapper = createWrapper();
        const field = wrapper.findFieldByName(name);

        expect(field.prop("type")).toBe("select");
        expect(field.prop("component")).toBe(Input.Select);
      }
    );

    it("renders gender field", () => {
      const wrapper = createWrapper();
      const field = wrapper.findFieldByName("gender");

      expect(field.prop("type")).toBe("select");
      expect(field.prop("component")).toBe(Input.Select);
    });

    it("renders save button", () => {
      const wrapper = createWrapper();
      const saveButton = wrapper.findSaveButton();

      expect(saveButton.prop("type")).toBe("submit");
      expect(saveButton.text()).toBe("Save");
    });
  });

  describe("form validation", () => {
    describe("name fields validation", () => {
      test.each([
        ["firstName", PERSONALINFO_FIRSTNAME_REQUIRED],
        ["lastName", PERSONALINFO_LASTNAME_REQUIRED]
      ])("name fields blank validation", (name: string, message: string) => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName(name);
        input.simulate("blur");

        const errorPresent = wrapper.findFormFeedback(message);
        expect(errorPresent).toBeTruthy();
      });

      test.each([
        ["firstName", PERSONALINFO_FIRSTNAME_INVALID],
        ["lastName", PERSONALINFO_LASTNAME_INVALID]
      ])("name fields invalid validation", (name: string, message: string) => {
        const wrapper = createWrapper();
        const input = wrapper.findInputByName(name);
        input.simulate("change", { target: { value: "_123" } });

        const errorPresent = wrapper.findFormFeedback(message);
        expect(errorPresent).toBeTruthy();
      });
    });
  });
});