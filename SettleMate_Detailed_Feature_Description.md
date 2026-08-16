# SettleMate --- Detailed Feature Description

## 1. Student Module

The Student Module is the main part of SettleMate. It allows students to
manage their account, find accommodation, search for roommates, buy and
sell student essentials, save listings, manage orders, and provide
reviews.

------------------------------------------------------------------------

## 1.1 Authentication

Authentication controls how students create and access their SettleMate
accounts.

### Login

**Fields** - Email - Password

**Functionality**

The student enters their registered email and password. The system
validates the credentials. If the credentials are correct, the student
is redirected to the Student Dashboard. If the credentials are
incorrect, an appropriate error message is displayed.

The login system also prevents unauthenticated users from accessing
student-only pages.

### Register

**Fields** - Full Name - Email - Mobile Number - Gender - Location /
City - Address - Profile Photo / Avatar - Password - Confirm Password

**Functionality**

A new student creates an account by entering their personal information.
The system should validate required fields, email format, mobile number,
password requirements, password confirmation, and email uniqueness.

The student's location/city can also be used when displaying relevant
roommate and property listings.

### Forgot Password

**Flow**

``` text
Forgot Password
      ↓
Enter Email
      ↓
Verify OTP
      ↓
Change Password
      ↓
Redirect to Login
```

**Step 1 --- Enter Email**

The student enters their registered email address.

**Step 2 --- OTP Verification**

The system verifies the OTP associated with the password-reset request.

**Step 3 --- Change Password**

After successful OTP verification, the student creates a new password.

**Step 4 --- Redirect**

After the password is successfully changed, the student is redirected to
the login page.

------------------------------------------------------------------------

# 1.2 Public Pages

These are the public-facing pages of SettleMate.

## Home

The Home page introduces SettleMate and provides access to its major
services:

-   PG / Flat Finder
-   Roommate Finder
-   Student Marketplace

It acts as the main entry point to the application.

## About Us

Explains:

-   What SettleMate is
-   Why it was created
-   The problem it solves
-   Who it is designed for
-   How it helps students settle into a new city

## Services

Presents the major services offered by SettleMate, primarily:

-   Accommodation discovery
-   Roommate discovery
-   Student marketplace

## Contact Us

Allows users to contact the SettleMate platform through a contact form
and relevant contact information.

Messages submitted through this page can be managed by the administrator
through the Contact Messages section.

## Reviews & Ratings

Provides a public area for displaying student feedback and ratings about
their experience with SettleMate.

------------------------------------------------------------------------

# 1.3 Student Dashboard

The Student Dashboard acts as the student's central control panel.

## Welcome Card

Displays a personalized welcome message and can show the student's
profile information.

## Latest Properties

Displays recently available property listings so students can quickly
discover new PGs and flats.

## Latest Marketplace Items

Displays recently listed marketplace products that students may want to
purchase.

## Roommate Suggestions

Displays available roommate listings that may be relevant to the
student.

## Quick Actions

Provides shortcuts to commonly used features such as:

-   Find PG / Flat
-   Find Roommate
-   Marketplace
-   My Listings
-   Wishlist
-   Orders

------------------------------------------------------------------------

# 1.4 Student Profile

The Profile section allows students to manage personal information and
their activities on SettleMate.

## A. Personal Information

### Edit Profile

Students can update their personal information such as:

-   Name
-   Mobile number
-   Gender
-   City
-   Address

### Change Photo

Students can upload or change their profile avatar.

### Change Password

Students can change their existing password.

Typical flow:

``` text
Current Password
       ↓
New Password
       ↓
Confirm Password
       ↓
Update Password
```

------------------------------------------------------------------------

## B. Saved List / Wishlist

The wishlist allows students to save listings for later access.

### Saved PGs

Students can save PG listings and access them later from their profile.

### Saved Places

Students can save property/place listings that interest them.

### Saved Marketplace Items

Students can save marketplace products for future consideration.

------------------------------------------------------------------------

## C. My Marketplace Listings

This section allows students to manage items they have listed for sale.

### Add Item

Creates a new marketplace listing.

### Edit Item

Updates an existing marketplace listing.

### Delete Item

Removes the student's marketplace listing.

------------------------------------------------------------------------

## D. My Roommate Listings

Allows students to manage their own roommate listings.

### Add Listing

Creates a new roommate listing.

### Edit Listing

Updates an existing roommate listing.

### Delete Listing

Removes the roommate listing.

------------------------------------------------------------------------

## E. My Reviews

Allows students to manage their submitted reviews.

### View Reviews

Displays reviews written by the student.

### Delete Reviews

Allows the student to delete their own reviews.

------------------------------------------------------------------------

## F. My Orders

Allows students to view marketplace orders they have placed.

The order information can include purchased items and the applicable
order status.

------------------------------------------------------------------------

# 1.5 Roommate Finder

The Roommate Finder helps students discover potential roommates through
available roommate listings.

It contains:

-   Listings
-   Details Page
-   My Listing

## Roommate Listings

### View All Listings

Students can browse available roommate listings.

A listing can display relevant information such as:

-   Profile photo
-   Name
-   Gender
-   Location
-   Budget
-   Other listing information

### Filter by Location

Students can filter roommate listings according to city/location.

### Filter by Gender

Students can filter available roommate listings according to gender.

### Filter by Budget

Students can filter listings according to their preferred budget.

------------------------------------------------------------------------

## Roommate Details Page

### View Profile

Displays the roommate's profile information.

### Lifestyle Preferences

Displays lifestyle-related information provided in the roommate listing
so students can determine whether the person may be suitable for them.

### Contact via Phone

Allows the student to contact the roommate using the provided phone
number.

### Contact via Email

Allows the student to contact the roommate through email.

------------------------------------------------------------------------

## My Listing

Students can manage their own roommate listing.

### Add Listing

Creates a new roommate listing.

### Edit Listing

Updates an existing roommate listing.

### Delete Listing

Removes the roommate listing.

------------------------------------------------------------------------

# 1.6 Flat / PG Finder

The Flat / PG Finder is responsible for accommodation discovery.

Students can find:

-   PGs
-   Flats

## Listings

### All PGs

Displays available PG property listings.

### All Flats

Displays available flat/rental-property listings.

------------------------------------------------------------------------

## Filters

### City

Filters properties according to city.

### Budget

Filters properties according to the student's preferred rent range.

### Gender

Filters properties according to the intended gender/category supported
by the listing.

### Furnished

Allows students to filter according to furnishing availability.

### Amenities

Allows students to filter properties based on available amenities.

------------------------------------------------------------------------

## Property Details

### Property Information

Displays important information such as:

-   Property name
-   Property type
-   Address
-   City
-   Rent
-   Deposit
-   Availability

### Images

Displays photographs of the property.

### Amenities

Displays facilities available at the property.

### Owner Contact

Displays relevant property-owner contact information.

### Call Owner

Allows the student to contact the owner by phone.

### Email Owner

Allows the student to contact the owner by email.

### Add to Wishlist

Allows the student to save the property for later.

------------------------------------------------------------------------

# 1.7 Student Marketplace

The Student Marketplace allows students to browse, buy, and sell
student-related items.

It contains:

-   Listings
-   Details
-   Cart
-   Checkout

## Marketplace Listings

### Browse Items

Students can browse available marketplace items.

Examples may include:

-   Books
-   Furniture
-   Electronics
-   Cycles
-   Appliances
-   Other student essentials

------------------------------------------------------------------------

## Marketplace Details

### Item Information

Displays information about the marketplace item, such as:

-   Item name
-   Description
-   Price
-   Condition/details
-   Image

### Seller Details

Displays relevant information about the student selling the item.

### Contact Seller

Allows an interested student to contact the seller.

------------------------------------------------------------------------

## Cart

### Add to Cart

Adds a marketplace item to the student's cart.

### Remove from Cart

Removes an item from the cart.

------------------------------------------------------------------------

## Checkout

The finalized marketplace payment method is:

**Cash on Pickup Only**

### Order Summary

Displays:

-   Items
-   Quantity
-   Item price
-   Total amount

### Confirm Order

The student confirms the marketplace order. The order is stored in the
system with cash-on-pickup as the payment method.

------------------------------------------------------------------------

# 1.8 Reviews & Ratings

Students can provide feedback through the Reviews module.

## Give Rating

The student selects a rating, such as a five-star rating.

## Write Review

The student writes feedback about their experience.

## Edit Review

The student can modify their previously submitted review.

## Delete Review

The student can remove their own review.

------------------------------------------------------------------------

# 2. Property Owner Module

The Property Owner Dashboard is designed for users who provide PGs or
flats.

It allows owners to manage their properties and view student enquiries.

------------------------------------------------------------------------

# 2.1 Owner Dashboard

## Overview

Provides a summary of the owner's property activity.

## Total Properties

Displays the total number of properties listed by the owner.

## Active Listings

Displays the number of properties currently active/available.

## Sold/Rented Properties

Displays properties that are no longer available because they have been
rented or sold.

## Pending Requests

Displays pending requests or enquiries requiring the owner's attention.

------------------------------------------------------------------------

# 2.2 Property Management

Property Management is the main functionality available to property
owners.

## My Properties

### View All Properties

The owner can view all properties they have added.

## Add Property

The owner can create a new property listing.

### Fields

-   Property Name
-   Property Type
-   Address
-   City
-   Rent
-   Deposit
-   Amenities
-   Images

### Property Name

The name/title used to identify the property.

### Property Type

Defines the type of property, such as PG or Flat.

### Address

Stores the property's address.

### City

Stores the city in which the property is located.

### Rent

Stores the rental amount.

### Deposit

Stores the required security deposit.

### Amenities

Stores facilities provided by the property.

### Images

Allows the owner to upload property photographs.

## Edit Property

Allows the owner to update an existing property listing, including
information such as rent, amenities, address, and images.

## Delete Property

Allows the owner to remove their property listing.

## Mark Available / Not Available

Allows the owner to control the property's availability.

**Available:** The property can be shown as available to students.

**Not Available:** The property is no longer available for students.

------------------------------------------------------------------------

# 2.3 Enquiries

The Enquiries section allows owners to view students who have shown
interest in their properties.

## View Student Enquiries

Displays information such as:

-   Student Name
-   Phone Number
-   Email
-   Inquiry Date

This provides owners with a centralized record of interested students.

------------------------------------------------------------------------

# 2.4 Owner Reviews

## View Property Reviews

Allows owners to view reviews students have submitted about their
property.

## Reply --- Optional

If implemented, the owner can respond to a student's property review.

------------------------------------------------------------------------

# 2.5 Owner Profile

## Personal Information

Allows the owner to manage personal account information.

## Business Information

Allows the owner to manage information related to their property/rental
business.

## Change Password

Allows the owner to update their account password.

------------------------------------------------------------------------

# 3. Admin Module

The Admin Dashboard is the central management area of SettleMate.

The administrator manages users, properties, marketplace listings,
roommate listings, reviews, contact messages, and reports.

------------------------------------------------------------------------

# 3.1 Admin Dashboard

The dashboard provides an overall snapshot of platform activity.

## Total Students

Displays the number of registered students.

## Total Property Owners

Displays the number of registered property owners.

## Total Properties

Displays the total number of property listings.

## Total Marketplace Listings

Displays the number of marketplace listings.

## Total Roommate Listings

Displays the number of roommate listings.

## Total Orders

Displays the number of marketplace orders.

## Total Reviews

Displays the number of submitted reviews.

------------------------------------------------------------------------

# 3.2 User Management

The administrator can manage both major user types.

## Students

### View

Admin can view student accounts.

### Edit

Admin can modify student information when required.

### Delete

Admin can remove a student account.

### Block

Admin can block a student account from accessing the platform.

## Property Owners

Admin can perform the same management operations for property-owner
accounts:

-   View
-   Edit
-   Delete
-   Block

------------------------------------------------------------------------

# 3.3 Property Management

This module allows administrators to control property listings.

## View Properties

Admin can view properties submitted by owners.

## Approve Property

Admin can approve a property after reviewing its information.

## Reject Property

Admin can reject a property that does not meet platform requirements.

## Delete Property

Admin can remove a property listing when necessary.

------------------------------------------------------------------------

# 3.4 Marketplace Management

Admin manages student marketplace listings.

## View Items

Admin can view marketplace products listed by students.

## Delete Item

Admin can remove inappropriate or invalid marketplace listings.

## Approve Item --- Optional

If implemented, marketplace items can go through an admin approval
process before becoming publicly visible.

------------------------------------------------------------------------

# 3.5 Roommate Listings Management

## View Listings

Admin can view roommate listings submitted by students.

## Remove Listing

Admin can remove inappropriate or invalid roommate listings.

------------------------------------------------------------------------

# 3.6 Reviews Management

## View Reviews

Admin can view submitted reviews.

## Delete Reviews

Admin can remove reviews when necessary according to platform rules.

------------------------------------------------------------------------

# 3.7 Contact Messages

Messages submitted through the Contact Us page can be managed here.

## View Messages

Admin can view contact messages and their relevant sender information.

## Reply --- Optional

If implemented, admin can respond to contact messages.

------------------------------------------------------------------------

# 3.8 Reports

The Reports section provides summarized information about the platform.

## Total Users

Displays the total number of users.

## Total Properties

Displays the total number of properties.

## Total Marketplace Sales

The finalized module defines this as the:

**Cash Orders Count**

It represents the number of confirmed marketplace orders using the
cash-on-pickup process.

## Most Viewed Property --- Optional

If implemented, the system can identify the property receiving the
highest number of views.

------------------------------------------------------------------------

# 3.9 Admin Profile

## Update Profile

Allows the administrator to update their profile information.

## Change Password

Allows the administrator to change their password.

------------------------------------------------------------------------

# 4. Complete Feature Relationship

The complete SettleMate system can be understood through three major
roles:

``` text
                         SETTLEMATE
                             |
              +--------------+--------------+
              |              |              |
              v              v              v
           STUDENT        OWNER           ADMIN
              |              |              |
       +------+------+       |       +------+--------+
       |      |      |       |       |      |        |
       v      v      v       v       v      v        v
   Roommate PG/Flat Market Property Users Property Reports
    Finder   Finder  place Management Management Management
       |       |       |       |       |       |
       v       v       v       v       v       v
    Contact  Contact  Cart   Enquiries Approve Reviews
    Student  Owner    Order
```

## Three Core Value Propositions

### 1. Find a Place

**Flat / PG Finder**

Students discover accommodation according to their requirements.

### 2. Find a Person

**Roommate Finder**

Students discover potential roommates through relevant roommate
listings.

### 3. Find Essentials

**Student Marketplace**

Students can buy and sell useful items and place cash-on-pickup orders.

These three core services are supported by:

-   Authentication
-   Profiles
-   Wishlist
-   Reviews
-   Orders
-   Property Owner Management
-   Admin Management

------------------------------------------------------------------------

# 5. Final Module Summary

  -----------------------------------------------------------------------
  Role                                Main Features
  ----------------------------------- -----------------------------------
  **Student**                         Authentication, Public Pages,
                                      Dashboard, Profile, Wishlist,
                                      Marketplace Listings, Roommate
                                      Listings, Roommate Finder, PG/Flat
                                      Finder, Marketplace, Cart,
                                      Checkout, Reviews, Orders

  **Property Owner**                  Dashboard, Property Management,
                                      Add/Edit/Delete Property,
                                      Availability, Enquiries, Reviews,
                                      Profile

  **Admin**                           Dashboard, User Management,
                                      Property Management, Marketplace
                                      Management, Roommate Listing
                                      Management, Reviews Management,
                                      Contact Messages, Reports, Admin
                                      Profile
  -----------------------------------------------------------------------

------------------------------------------------------------------------

## Source of Functional Scope

This document is based on the finalized **SettleMate Page Modules**
file. The functional scope above follows that module list rather than
adding older or unfinalized functionality.
