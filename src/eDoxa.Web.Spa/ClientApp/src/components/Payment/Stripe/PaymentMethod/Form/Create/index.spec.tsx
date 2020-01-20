import React from "react";
import Create from ".";
import { configureStore } from "store";
import { Provider } from "react-redux";

const shallow = global["shallow"];

const store = configureStore();

//  const createWrapper = (): ReactWrapper | any => {
//    return mount(
//     <Provider store={store}>
//       <Create />
//     </Provider>;
//    );
// };

describe("<PaymentMethodCreateForm />", () => {
  it("should match the snapshot", () => {
    const shallowWrapper = shallow(
      <Provider store={store}>
        <Create />
      </Provider>
    );
    expect(shallowWrapper).toMatchSnapshot();
  });

  // describe("defines paymentMethod create form fields", () => {
  //   it("renders save button", () => {
  //     const wrapper = createWrapper();
  //     const saveButton = wrapper.find("SaveButton").first();
  //     const button = saveButton.find("button").first();

  //     expect(button.prop("type")).toBe("submit");
  //     expect(button.text()).toBe("Save");
  //   });

  //   it("renders cancel button", () => {
  //     const wrapper = createWrapper();
  //     const cancelButton = wrapper.find("CancelButton").first();
  //     const button = cancelButton.find("button").first();

  //     expect(button.prop("type")).toBe("button");
  //     expect(button.text()).toBe("Cancel");
  //   });
  // });
});
