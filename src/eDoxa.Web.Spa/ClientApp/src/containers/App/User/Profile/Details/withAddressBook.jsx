import React, { Component } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { loadAddressBook } from "../../../../../store/actions/identityActions";
import { CREATE_ADDRESS_MODAL, UPDATE_ADDRESS_MODAL, DELETE_ADDRESS_MODAL } from "../../../../../modals/Identity/Address/modals";

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
        showCreateAddressModal: () => dispatch(show(CREATE_ADDRESS_MODAL)),
        showUpdateAddressModal: addressId => dispatch(show(UPDATE_ADDRESS_MODAL, { addressId })),
        showDeleteAddressModal: addressId => dispatch(show(DELETE_ADDRESS_MODAL, { addressId }))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(AddressBookContainer);
};

export default withAddressBook;
