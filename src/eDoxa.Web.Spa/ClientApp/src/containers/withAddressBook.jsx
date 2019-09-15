import React, { Component } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { SubmissionError } from "redux-form";
import actions from "../actions/identity";
import { loadAddressBook, addAddress, updateAddress, removeAddress } from "../actions/identity/creators";
import { CREATE_ADDRESS_MODAL } from "../modals";

const withAddressBook = WrappedComponent => {
  class AddressBookContainer extends Component {
    componentDidMount() {
      this.props.actions.loadAddressBook();
    }

    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

  const mapStateToProps = state => {
    return {
      addressBook: state.user.addressBook
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadAddressBook: () => dispatch(loadAddressBook()),
        addAddress: async data => {
          await dispatch(addAddress(data)).then(async action => {
            switch (action.type) {
              case actions.ADD_ADDRESS_SUCCESS:
                await dispatch(loadAddressBook());
                break;
              case actions.ADD_ADDRESS_FAIL:
                const { isAxiosError, response } = action.error;
                if (isAxiosError) {
                  throw new SubmissionError(response.data.errors);
                }
                break;
              default:
                console.error(action);
                break;
            }
          });
        },
        updateAddress: async (addressId, data) => {
          await dispatch(updateAddress(addressId, data)).then(async action => {
            switch (action.type) {
              case actions.UPDATE_ADDRESS_SUCCESS:
                await dispatch(loadAddressBook());
                break;
              case actions.UPDATE_ADDRESS_FAIL:
                const { isAxiosError, response } = action.error;
                if (isAxiosError) {
                  throw new SubmissionError(response.data.errors);
                }
                break;
              default:
                console.error(action);
                break;
            }
          });
        },
        removeAddress: addressId => dispatch(removeAddress(addressId)),
        showCreateAddressModal: () => dispatch(show(CREATE_ADDRESS_MODAL))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(AddressBookContainer);
};

export default withAddressBook;
