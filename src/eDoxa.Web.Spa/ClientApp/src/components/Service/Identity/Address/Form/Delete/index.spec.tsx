it("should match the snapshot", () => {});

// import React from "react";
// import { Provider } from "react-redux";
// import { ReactWrapper } from "enzyme";
// import Delete from ".";
// import store from "store";

// const shallow = global["shallow"];
// const mount = global["mount"];

//

// const createWrapper = (): ReactWrapper | any => {
//   return mount(
//     <Provider store={store}>
//       <Delete addressId="addressId" handleCancel={() => {}} />
//     </Provider>
//   );
// };

// describe("<UserAddressDeleteForm />", () => {
//   it("should match the snapshot", () => {
//     const shallowWrapper = shallow(
//       <Delete addressId="addressId" handleCancel={() => {}} />
//     );
//     expect(shallowWrapper).toMatchSnapshot();
//   });

//   describe("defines address delete form fields", () => {
//     it("renders save button", () => {
//       const wrapper = createWrapper();
//       const saveButton = wrapper.find("SaveButton").first();
//       const button = saveButton.find("button").first();

//       expect(button.prop("type")).toBe("submit");
//       expect(button.text()).toBe("Save");
//     });

//     it("renders cancel button", () => {
//       const wrapper = createWrapper();
//       const cancelButton = wrapper.find("CancelButton").first();
//       const button = cancelButton.find("button").first();

//       expect(button.prop("type")).toBe("button");
//       expect(button.text()).toBe("Cancel");
//     });
//   });
// });
