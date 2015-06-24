using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProviderHostedAppPartsWeb.Models {

  public class Customer {
    [DisplayName("First Name")]
    public string FirstName { get; set; }
    [DisplayName("Last Name")]
    public string LastName { get; set; }
  }

  public class SampleData {

    public static IEnumerable<Customer> GetCustomers() {
      return new List<Customer> { 
        new Customer{FirstName="Bob", LastName="Fox"},
        new Customer{FirstName="Max", LastName="Mart"},
        new Customer{FirstName="Betty", LastName="Boop"},
        new Customer{FirstName="Oh", LastName="Noyoudont"},
        new Customer{FirstName="Rudy", LastName="Haggerty"}
      };
    }
  }
}