using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework {

    public class Numbers {

        public IEnumerable<string> SubstitutedNumbers(int maxNumber, IDictionary<int, string> numberSubstitutions) {
            if(maxNumber < 1) {
                throw new ArgumentOutOfRangeException("Must be positive number", nameof(maxNumber));
            }

            IEnumerable<int> numberSequence = Enumerable.Range(1, maxNumber);

            if(numberSubstitutions != null && numberSubstitutions.Any()) {
                // filter out Keys outside range [1, maxNumber]
                IEnumerable<KeyValuePair<int, string>> substitutions = numberSubstitutions.Where(x => x.Key > 0 && x.Key <= maxNumber); 

                if(substitutions.Any()) {
                    return numberSequence
                        .Select(x => new {
                                Number = x,
                                Substitution = substitutions
                                    .Where(y => (x % y.Key) == 0)
                                    .Select(y => y.Value)
                            })
                        .Select(z => z.Substitution.Any() ? string.Join("", z.Substitution) : z.Number.ToString());
                }
            }

            // no substitutes, or substitute numbers are larger than maxNumber just return numbers
            return numberSequence.Select(x => x.ToString());
        }
    }
}
